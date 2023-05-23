using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Com.Okmer.GameController;
using System.IO.Ports;
using System.Net.Configuration;

namespace EOD_WinGUI
{
    public partial class X : Form
    {
        public XBoxController controller { get; set; } = new XBoxController();

        public X()
        {
            InitializeComponent();
            Log.View = View.Details;
            Log.Columns.Add("Timestamp").Width = 75;
            Log.Columns.Add("Message").Width = 300;


            controller.Connection.ValueChanged += (s, e) => logConnection();

            controller.LeftTrigger.ValueChanged += (s, e) =>
            {
                int val = (int)(controller.LeftTrigger.Value * 100);
                SendMotor(1, val);
                MessageBox.Show(val.ToString());
            };
        }

        private void UpdateLeftTrigger(float value)
        {
            MessageBox.Show(value.ToString());
            Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), "Ready to pair" }));
            Log.Update();
            LeftPos.Value = ((int)(value * 100));
        }

        private void UpdateRightTrigger(float value)
        {
            RightPos.Value = ((int)(value * 100));
        }

        private void Form1_Load(object sender, EventArgs ev)
        {
            Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), "Ready to pair" }));
           
           

            foreach (string port in SerialPort.GetPortNames())
            {
                var tsb = new ToolStripButton(port);
                tsb.Click += (s, e) => ConnectToPort(port);
                ComPortList.DropDownItems.Add(tsb);
            }
        }


        private void LeftRumble_Scroll(object sender, EventArgs e)
        {
            controller.LeftRumble.Rumble((float)LeftRumble.Value/100);
        }



        private void RightRumble_Scroll(object sender, EventArgs e)
        {
            controller.RightRumble.Rumble((float)RightRumble.Value/100);
        }


        private void LeftRumble_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { LeftRumble.Value = 0; }
            LeftRumble_Scroll(sender, e);
        }
        private void RightRumble_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {RightRumble.Value = 0; }
            RightRumble_Scroll(sender, e);
        }



        private void LeftShoulder_Click(object sender, EventArgs e)
        {
            LeftPos.Value = 100;
            Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), $"Clamp left" })).EnsureVisible();
        }

        private void RightShoulder_Click(object sender, EventArgs e)
        {
            RightPos.Value = 100;
            Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), $"Clamp right" })).EnsureVisible();
        }


        private void ConnectToPort(string port)
        {
            if(Arduino.IsOpen) 
            { 
                Arduino.Close();
                Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), $"Connection closed on {Arduino.PortName}" })).EnsureVisible();
            };
            try
            {
                Arduino.BaudRate = 115200;
                Arduino.PortName = port;
                Arduino.Open();
                Arduino.ReceivedBytesThreshold = 20;
                Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), $"Connection established on {port}" })).EnsureVisible();
            }
            catch (Exception ex)
            {
                Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), $"Can't connect to {port} stacktrace: {ex.StackTrace}" })).EnsureVisible();
            }
            
            
        }

        private void LeftPos_Scroll(object sender, EventArgs e)
        {
            SendMotor(1, LeftPos.Value);
        }

        private void RightPos_Scroll(object sender, EventArgs e)
        {
            SendMotor(2, RightPos.Value);
        }

        private void SendMotor(byte motor, int speed)
        {
            if (Arduino.IsOpen)
            {
                byte[] par1 = new byte[] {motor};
                byte[] msg = par1.Concat(BitConverter.GetBytes((char)speed)).ToArray();
                Arduino.Write(msg, 0, 2);
                Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), $"Sended {speed} to motor {motor}" })).EnsureVisible();
            }
        }

        private void logConnection()
        {
            Log.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("HH:mm:ss:ff"), $"New Connection status {controller.Connection.Value}" })).EnsureVisible();
        }
    }
}
