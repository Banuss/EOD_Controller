using EOD_WPF.GameController;
using System.Windows;
using System.Windows.Media;
using System.IO.Ports;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using System.Net.Sockets;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using EOD_WPF.Model;

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

    
        public MainWindow()
        {
            InitializeComponent();
            franka = new Franka(viewPort3d);

            var guiDisp = Application.Current.Dispatcher;

            controller.Connection.ValueChanged += (s, e) => {
                new LogItem($"Controller {controller.Connection}").print(log);
                new LogItem($"Battery {controller.Battery.Value}").print(log);
                };

            Arduino.ErrorReceived += (s, e) => guiDisp.Invoke(() => { new LogItem($"Error {e.ToString()}").print(log); });

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateMotors);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();

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
                RotationJ1.Value = controller.LeftThumbstick.Value.X * RotationJ1.Maximum;
                RotationJ2.Value = controller.LeftThumbstick.Value.Y * RotationJ2.Maximum;
            });

            controller.RightThumbstick.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                RotationJ3.Value = controller.RightThumbstick.Value.X * RotationJ3.Maximum;
                RotationJ4.Value = controller.RightThumbstick.Value.Y * RotationJ4.Maximum;
            });

            controller.RightTrigger.ValueChanged += (s, e) => guiDisp.Invoke(() => 
            {   
                if (e.Value >= RightTrigger.Value)
                {
                    RightTrigger.Value = e.Value;
                }
            });

            controller.LeftShoulder.ValueChanged += (s,e) => guiDisp.Invoke(() => 
            {
                LeftTrigger.Value = 0;
            });

            controller.RightShoulder.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                RightTrigger.Value = 0;
            });

            controller.Y.ValueChanged += (s, e) => guiDisp.Invoke(() =>
            {
                invert = e.Value;
            });
        }

        private void LeftShoulder_ValueChanged(object? sender, ValueChangeArgs<bool> e)
        {
            throw new NotImplementedException();
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
            if (Arduino.IsOpen)
            {
                byte[] par1 = new byte[] { motor };
                byte[] msg = par1.Concat(BitConverter.GetBytes((char)speed)).ToArray();
                Arduino.Write(msg, 0, 2);
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
            };
            try
            {
                Arduino.BaudRate = 115200;
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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            franka.joints[1].angle = RotationJ1.Value;
            franka.joints[2].angle = RotationJ2.Value;
            franka.joints[3].angle = RotationJ3.Value;
            franka.joints[4].angle = RotationJ4.Value;
            if ((bool)Simulate.IsChecked)
            {
                franka.execute_fk();
            }
            
        }
    }
}
