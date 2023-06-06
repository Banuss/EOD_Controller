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

        bool hold_A;

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

            Arduino.ErrorReceived += (s, e) => guiDisp.Invoke(() => { new LogItem($"Error {e}").print(log); });


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

            //Simulation Update Timer
            DispatcherTimer updateLocation = new DispatcherTimer();
            updateLocation.Tick += new EventHandler(UpdateLocation);
            updateLocation.Interval = new TimeSpan(0, 0, 0, 0, 10); //100Hz
            updateLocation.Start();


            //Filling Comport Selector and setup comport
            foreach (string port in SerialPort.GetPortNames())
            {
                comports.Items.Add(port);
            }

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

            Arduino.DataReceived += (s, e) => guiDisp.Invoke(() => 
            {
                ma1.Content = Arduino.ReadChar();

            });



        }


        private void UpdateLocation(object sender, EventArgs args)
        {
            if((bool)Joint.IsChecked && (bool)Live.IsChecked)
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
                franka.execute_fk();
            }

            else if (!(bool)Joint.IsChecked && (bool)Live.IsChecked)
            {
                if (Math.Abs(SpeedX.Value) > drift) SliderX.Value += SpeedX.Value;
                if (Math.Abs(SpeedY.Value) > drift) SliderY.Value += SpeedY.Value;
                if (Math.Abs(SpeedZ.Value) > drift) SliderZ.Value += SpeedZ.Value;
                franka.execute_ik(new Vector3D(SliderX.Value, SliderY.Value, SliderZ.Value));
            }

            xlabel.Content = $"X: {franka.headPoint.X}";
            ylabel.Content = $"Y: {franka.headPoint.Y}";
            zlabel.Content = $"Z: {franka.headPoint.Z}";
        }

        private void UpdateMotors(object sender, EventArgs args)
        {
            if (Arduino.IsOpen)
            {
                SendMotor(1, (int)(LeftTrigger.Value * 180));
                SendMotor(2, (int)(RightTrigger.Value * 180));
            }
        }

        private void SendMotor(byte motor, int speed)
        {
            try
            {
                byte[] par1 = new byte[] { motor };
                byte[] msg = par1.Concat(BitConverter.GetBytes((char)speed)).ToArray();
                Arduino.Write(msg, 0, 2);
            }
            catch (Exception ex)
            {
                new LogItem($"Error Stacktrace: {ex.StackTrace}").print(log);
            }
        }

        private void comports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConnectToPort(comports.SelectedItem.ToString());
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
                Arduino.BaudRate = 115200;
                Arduino.ReadTimeout = 300;
                Arduino.PortName = port;
                Arduino.Open();
                Arduino.ReceivedBytesThreshold = 20;
                new LogItem($"Connection established on {port}").print(log);
            }
            catch (Exception ex)
            {
                new LogItem($"Can't connect to {port} stacktrace: {ex.StackTrace}").print(log);
                Arduino.Close();
            }
        }

        private void LeftRumble_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            controller.LeftRumble.Rumble((float)RoughRumble.Value);
        }

        private void RightRumble_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            controller.RightRumble.Rumble((float)FineRumble.Value);
        }

        private void MapController()
        {
            controller.LeftTrigger.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                if (!invert && e.Value >= LeftTrigger.Value)
                {
                    LeftTrigger.Value = e.Value;
                }

                if (invert)
                {
                    LeftTrigger.Value -= e.Value;
                }
            });

            controller.LeftThumbstick.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                //XYZ MODE
                if((bool)!Joint.IsChecked)
                {
                    SpeedX.Value =  controller.LeftThumbstick.Value.X * SpeedX.Maximum;
                    SpeedY.Value =  controller.LeftThumbstick.Value.Y * SpeedY.Maximum;
                }
                //JOINT SPACE
                else
                {
                    SpeedJ1.Value = controller.LeftThumbstick.Value.X * SpeedJ1.Maximum;
                    SpeedJ2.Value = controller.LeftThumbstick.Value.Y * SpeedJ2.Maximum;
                }            
            });

            controller.RightThumbstick.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                //XYZ MODE
                if ((bool)!Joint.IsChecked)
                {
                    SpeedZ.Value = controller.RightThumbstick.Value.Y * SpeedZ.Maximum;
                }
                //JOINT SPACE
                else
                {
                    SpeedJ4.Value = controller.RightThumbstick.Value.X * SpeedJ4.Maximum;
                    SpeedJ5.Value = controller.RightThumbstick.Value.Y * SpeedJ5.Maximum;
                }
            });

            controller.Start.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                if(controller.Start.Value)
                {
                    Live.IsChecked = !Live.IsChecked;
                }
            });


            controller.Back.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                if (controller.Back.Value)
                {
                    Joint.IsChecked = !Joint.IsChecked;
                }
            });

            controller.RightTrigger.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                if (e.Value >= RightTrigger.Value)
                {
                    RightTrigger.Value = e.Value;
                }
            });

            controller.RightShoulder.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                RightTrigger.Value = 0;
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
    }
}
