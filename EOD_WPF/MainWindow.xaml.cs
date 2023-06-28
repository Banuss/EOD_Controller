using EOD_WPF.GameController;
using System.Windows;
using System.IO.Ports;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using HelixToolkit.Wpf;
using EOD_WPF.Model;
using EOD_WPF.Remote;
using System.Windows.Media.Media3D;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Versioning;
using System.Reactive;
using System.Collections;
using System.Windows.Documents;
using System.Windows.Media.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Interop;
using System.IO;
using System.Security.RightsManagement;
using Newtonsoft.Json.Linq;

namespace EOD_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XBoxController controller = new XBoxController();
        SerialPort Arduino = new SerialPort();
        bool invert = false;
        Franka franka = null;
        Rumble rumble = null;
        Dispatcher guiDisp = Application.Current.Dispatcher;
        int maxspeed = 1;
        double drift = 0.5;

        byte[] buffer = new byte[8];
        Action kickoffRead = null;


        int encoder1 = 0;
        int encoder2 = 0;

        public event EventHandler FireAsync;
        Task updaterumble;

        public MainWindow()
        {
            InitializeComponent();
            franka = new Franka(viewPort3d);
            MapController();

            controller.Connection.ValueChanged += (s, e) => 
            {
                new LogItem($"Controller {controller.Connection}").print(log);
                new LogItem($"Battery {controller.Battery.Value}").print(log);
            };


            //Lock Joint 3
            RotationJ3.IsEnabled = false;
            SpeedJ3.IsEnabled = false;

            //Setting Slider Settings
            RotationJ1.Maximum = franka.joints[1].angleMax;
            RotationJ1.Minimum = franka.joints[1].angleMin;
            RotationJ1.Value = (RotationJ1.Maximum + RotationJ1.Minimum) / 2; 
            RotationJ2.Maximum = franka.joints[2].angleMax;
            RotationJ2.Minimum = franka.joints[2].angleMin;
            RotationJ2.Value = (RotationJ2.Maximum + RotationJ2.Minimum) / 2;
            RotationJ3.Maximum = franka.joints[3].angleMax;
            RotationJ3.Minimum = franka.joints[3].angleMin;
            RotationJ3.Value = (RotationJ3.Maximum + RotationJ3.Minimum) / 2;
            RotationJ4.Maximum = franka.joints[4].angleMax;
            RotationJ4.Minimum = franka.joints[4].angleMin;
            RotationJ4.Value = (RotationJ4.Maximum + RotationJ4.Minimum) / 2;
            RotationJ5.Maximum = franka.joints[5].angleMax;
            RotationJ5.Minimum = franka.joints[5].angleMin;
            RotationJ5.Value = (RotationJ5.Maximum + RotationJ5.Minimum) / 2;
            RotationJ6.Maximum = franka.joints[6].angleMax;
            RotationJ6.Minimum = franka.joints[6].angleMin;
            RotationJ6.Value = (RotationJ6.Maximum + RotationJ6.Minimum) / 2;
            RotationJ7.Maximum = franka.joints[7].angleMax;
            RotationJ7.Minimum = franka.joints[7].angleMin;
            RotationJ7.Value = (RotationJ7.Maximum + RotationJ7.Minimum) / 2;
            RotationGripper.Maximum = franka.joints[8].angleMax;
            RotationGripper.Minimum = franka.joints[8].angleMin;
            RotationGripper.Value = (RotationGripper.Maximum + RotationGripper.Minimum) / 2;

            SpeedJ1.Maximum = maxspeed;
            SpeedJ1.Minimum = -maxspeed;
            SpeedJ2.Maximum = maxspeed;
            SpeedJ2.Minimum = -maxspeed;
            SpeedJ3.Maximum = maxspeed;
            SpeedJ3.Minimum = -maxspeed;
            SpeedJ4.Maximum = maxspeed;
            SpeedJ4.Minimum = -maxspeed;
            SpeedJ5.Maximum = maxspeed;
            SpeedJ5.Minimum = -maxspeed;
            SpeedJ6.Maximum = maxspeed;
            SpeedJ6.Minimum = -maxspeed;
            SpeedJ7.Maximum = maxspeed;
            SpeedJ7.Minimum = -maxspeed;

            SpeedRotationG.Minimum = -maxspeed;
            SpeedRotationG.Maximum = maxspeed;
            SpeedClampG.Minimum = -maxspeed;
            SpeedClampG.Maximum = maxspeed;

            SpeedX.Maximum = maxspeed;
            SpeedX.Minimum = -maxspeed;
            SpeedY.Maximum = maxspeed;
            SpeedY.Minimum = -maxspeed;
            SpeedZ.Maximum = maxspeed;
            SpeedZ.Minimum = -maxspeed;

            //Message Send Timer
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateMotors);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10); //100Hz
            dispatcherTimer.Start();

            DispatcherTimer dispatcherTimerUI = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateUI);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50); //100Hz
            dispatcherTimer.Start();

           


            //Filling Comport Selector and setup comport
            fill_portnames();

            var gridView = new GridView();
            this.log.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Timestamp",
                DisplayMemberBinding = new Binding("Timestamp")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Message",
                DisplayMemberBinding = new Binding("Message")
            });

            franka.ForwardKinematics(franka.joints.Select(x => x.angle).ToArray());

            //Simulation Update Timer
            DispatcherTimer updateLocation = new DispatcherTimer();
            updateLocation.Tick += new EventHandler(UpdateLocation);
            updateLocation.Interval = new TimeSpan(0, 0, 0, 0, 10); //100Hz
            updateLocation.Start();


            FireAsync += HandleFireAsync;

        }


        private void fill_portnames()
        {
            comports.Items.Clear();
            foreach (string port in SerialPort.GetPortNames())
            {
                comports.Items.Add(port);
            }
        }

        private void UpdateLocation(object sender, EventArgs args)
        {
            xlabel.Content = $"X: {franka.headPoint.X.ToString("N0")}";
            ylabel.Content = $"Y: {franka.headPoint.Y.ToString("N0")}";
            zlabel.Content = $"Z: {franka.headPoint.Z.ToString("N0")}";
            xball.Content = $"X: {franka.reachingPoint.X.ToString("N0")}";
            yball.Content = $"Y: {franka.reachingPoint.Y.ToString("N0")}";
            zball.Content = $"Z: {franka.reachingPoint.Z.ToString("N0")}";

            if ((bool)Joint.IsChecked && (bool)Live.IsChecked)
            {
                if (Math.Abs(SpeedJ1.Value) > drift) RotationJ1.Value += SpeedJ1.Value;
                if (Math.Abs(SpeedJ2.Value) > drift) RotationJ2.Value += SpeedJ2.Value;
                if (Math.Abs(SpeedJ3.Value) > drift) RotationJ3.Value += SpeedJ3.Value;
                if (Math.Abs(SpeedJ4.Value) > drift) RotationJ4.Value += SpeedJ4.Value;
                if (Math.Abs(SpeedJ5.Value) > drift) RotationJ5.Value += SpeedJ5.Value;
                if (Math.Abs(SpeedJ6.Value) > drift) RotationJ6.Value += SpeedJ6.Value;
                if (Math.Abs(SpeedJ7.Value) > drift) RotationJ7.Value += SpeedJ7.Value;

                franka.joints[1].angle = RotationJ1.Value;
                franka.joints[2].angle = RotationJ2.Value;
                franka.joints[3].angle = RotationJ3.Value;
                franka.joints[4].angle = RotationJ4.Value;
                franka.joints[5].angle = RotationJ5.Value;
                franka.joints[6].angle = RotationJ6.Value;
                franka.joints[7].angle = RotationJ7.Value;

                //Rotation
                franka.joints[9].angle = RotationGripper.Value;

                franka.ForwardKinematics(franka.joints.Select(x => x.angle).ToArray());
            }

            else if (!(bool)Joint.IsChecked && (bool)Live.IsChecked)
            {
                if (Math.Abs(SpeedX.Value) > drift) SliderX.Value += SpeedX.Value * 5;
                if (Math.Abs(SpeedY.Value) > drift) SliderY.Value += SpeedY.Value * 5;
                if (Math.Abs(SpeedZ.Value) > drift) SliderZ.Value += SpeedZ.Value * 5;
                franka.updatecircle(new Vector3D(SliderX.Value, SliderY.Value, SliderZ.Value));
                franka.InverseKinematics();
            }

            //RotationJ1.Value = franka.joints[1].angle;
            //RotationJ2.Value = franka.joints[2].angle;
            //RotationJ3.Value = franka.joints[3].angle;
            //RotationJ4.Value = franka.joints[4].angle;
            //RotationJ5.Value = franka.joints[5].angle;
            //RotationJ6.Value = franka.joints[6].angle;
            //RotationJ7.Value = franka.joints[7].angle;

        }

        private void UpdateMotors(object sender, EventArgs args)
        {
            if (Arduino.IsOpen)
            {
                SendMotor(false, SpeedClampG.Value > 0, false, (int)(Math.Abs(SpeedClampG.Value) * 255));
                SendMotor(true, RotationSpeed.Value < 0, false, (int)(Math.Abs(RotationSpeed.Value) * 255));
            }
        }

        private void UpdateUI(object sender, EventArgs args)
        {
            if(Arduino.IsOpen)
            {
                ma1.Content = $"F CH1:{buffer[0]}";
                ro1.Content = $"R CH1:{BitConverter.ToInt16(buffer, 1)}";
                ma2.Content = $"F CH2:{buffer[3]}";
                ro2.Content = $"R CH2:{BitConverter.ToInt16(buffer, 4)}";
                FireAsync?.Invoke(this, EventArgs.Empty);
            }
        }


        public async void HandleFireAsync(object sender, EventArgs e)
        {
            if (updaterumble?.IsCompleted ?? true)
                updaterumble = SendRumble();
        }

        private async Task SendRumble()
        {
            if ((buffer[0] > 0))
            {
                controller.RightRumble.Value = 255 / buffer[0];
                await Task.Delay(500);
                return;
            }
            controller.RightRumble.Value = 0;
        }

        private void SendMotor(bool id, bool dir, bool ebrake, int speed)
        {
            try
            {
                byte[] msg = new byte[2];
                var bitArray = new BitArray(8);
                bitArray.Set(0, id);
                bitArray.Set(1, dir);
                bitArray.Set(2, ebrake);
                bitArray.CopyTo(msg,0);
                msg[1] = ((byte)(speed));
                Arduino.Write(msg, 0, 2);
            }
            catch (Exception ex)
            {
                new LogItem($"Error Stacktrace: {ex.StackTrace}").print(log);
            }
        }

        private void comports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comports.Items.Count > 0) {
                ConnectToPort(comports.SelectedItem.ToString());
            }
        }

      
        private void ConnectToPort(string port)
        {
            if (Arduino.IsOpen)
            {
                Arduino.Close();
                new LogItem($"Connection closed on {Arduino.PortName}").print(log);
            }
            try
            {
                Arduino.ErrorReceived += (s, e) => guiDisp.Invoke(() => { new LogItem($"Error {e}").print(log); });
                Arduino.BaudRate = 115200;
                Arduino.ReadTimeout = 0;
                Arduino.PortName = port;
                Arduino.DataReceived += Arduino_DataReceived;
                Arduino.Open();
                new LogItem($"Connection established on {port}").print(log);
            }
            catch (Exception ex)
            {
                new LogItem($"Can't connect to {port} stacktrace: {ex.StackTrace}").print(log);
                Arduino.Close();
            }
        }

        private void Arduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialDevice = sender as SerialPort;
            var buffer = new byte[serialDevice.BytesToRead];
            serialDevice.Read(buffer, 0, buffer.Length);
            if (buffer.Length > 7 && buffer[6] == 128 && buffer[7] ==128)
            {
                Array.Copy(buffer, this.buffer, 8);
            }
        }

        private void MapController()
        {
            controller.LeftThumbstick.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                //Gripper Mode
                if ((bool)Gripper.IsChecked)
                {
                    ClampSpeed.Value = controller.LeftThumbstick.Value.X;
                }

                //XYZ MODE
                
                //{
                //    SpeedX.Value =  controller.LeftThumbstick.Value.X * SpeedX.Maximum;
                //    SpeedY.Value =  controller.LeftThumbstick.Value.Y * SpeedY.Maximum;
                //}

                //JOINT SPACE
                else if ((bool)Joint.IsChecked)
                {
                    SpeedJ1.Value = controller.LeftThumbstick.Value.X * SpeedJ1.Maximum;
                    SpeedJ2.Value = controller.LeftThumbstick.Value.Y * SpeedJ2.Maximum;
                }            
            });

            controller.RightThumbstick.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                //Gripper Mode
                if ((bool)Gripper.IsChecked)
                {
                    RotationSpeed.Value = controller.RightThumbstick.Value.X;
                }

                if ((bool)GripperCopy.IsChecked)
                {
                    
                }

                //XYZ MODE
                
                //{
                //    SpeedZ.Value = controller.RightThumbstick.Value.Y * SpeedZ.Maximum;
                //}
                //JOINT SPACE
                else if ((bool)Joint.IsChecked)
                {
                    SpeedJ4.Value = controller.RightThumbstick.Value.X * SpeedJ4.Maximum;
                    SpeedJ5.Value = controller.RightThumbstick.Value.Y * SpeedJ5.Maximum;
                }
            });


            controller.LeftTrigger.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                SpeedClampG.Value = controller.RightTrigger.Value - controller.LeftTrigger.Value;
            });

            controller.RightTrigger.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                SpeedClampG.Value = controller.RightTrigger.Value - controller.LeftTrigger.Value;
            });

            controller.Start.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                if(controller.Start.Value)
                {
                    Follow.IsChecked = !Follow.IsChecked;
                }
            });


            controller.Back.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                if (controller.Back.Value)
                {
                    Joint.IsChecked = !Joint.IsChecked;
                }
            });

            //DPAD
            controller.Left.ValueChanged += (s, e) => guiDisp.Invoke(() => { RotationJ7.Value -= 1; });
            controller.Right.ValueChanged += (s, e) => guiDisp.Invoke(() => { RotationJ7.Value += 1; });
            controller.Up.ValueChanged += (s, e) => guiDisp.Invoke(() => { RotationJ6.Value += 1; });
            controller.Down.ValueChanged += (s, e) => guiDisp.Invoke(() => { RotationJ6.Value -= 1; });
        }


        public int radtodeg(double rad)
        {
            return (int)(rad * 180 / double.Pi);
        }

        public class LogItem
        {
            public DateTime Timestamp { get; set; }

            public string Message { get; set; }

            public LogItem(string message)
            {
                this.Timestamp = DateTime.Now;
                this.Message = message;
            }

            public void print(ListView receiver)
            {
                receiver.Items.Add(this);
            }
        }

        private void resetSender(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((Slider)sender).Value = 0;
        }

        private void RefreshCOM_Click(object sender, RoutedEventArgs e)
        {
            fill_portnames();
        }

        private void StopCOM_Click(object sender, RoutedEventArgs e)
        {
            SendMotor(true, true, false, 0);
            SendMotor(false, true, false, 0);
            Arduino.Close();
            new LogItem($"Connection closed on {Arduino.PortName}").print(log);
        }

        private void RotationSpeed_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((Slider)sender).Value = 0;
        }

        private void MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Slider)sender).Value = 0;
        }
    }
}
