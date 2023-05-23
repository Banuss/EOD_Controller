namespace EOD_WinGUI
{
    partial class X
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LeftShoulder = new System.Windows.Forms.Button();
            this.RightShoulder = new System.Windows.Forms.Button();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.controllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSBPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSB1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSB2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSB3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSB4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endEffectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ComPortList = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.LeftPos = new System.Windows.Forms.TrackBar();
            this.RightPos = new System.Windows.Forms.TrackBar();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Log = new System.Windows.Forms.ListView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LeftRumble = new System.Windows.Forms.TrackBar();
            this.RightRumble = new System.Windows.Forms.TrackBar();
            this.LeftPosLive = new System.Windows.Forms.TrackBar();
            this.RightPosLive = new System.Windows.Forms.TrackBar();
            this.Arduino = new System.IO.Ports.SerialPort(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftRumble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightRumble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPosLive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPosLive)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftShoulder
            // 
            this.LeftShoulder.Location = new System.Drawing.Point(12, 163);
            this.LeftShoulder.Name = "LeftShoulder";
            this.LeftShoulder.Size = new System.Drawing.Size(82, 62);
            this.LeftShoulder.TabIndex = 0;
            this.LeftShoulder.Text = "Grab";
            this.LeftShoulder.UseVisualStyleBackColor = true;
            this.LeftShoulder.Click += new System.EventHandler(this.LeftShoulder_Click);
            // 
            // RightShoulder
            // 
            this.RightShoulder.Location = new System.Drawing.Point(334, 163);
            this.RightShoulder.Name = "RightShoulder";
            this.RightShoulder.Size = new System.Drawing.Size(84, 62);
            this.RightShoulder.TabIndex = 1;
            this.RightShoulder.Text = "Grab";
            this.RightShoulder.UseVisualStyleBackColor = true;
            this.RightShoulder.Click += new System.EventHandler(this.RightShoulder_Click);
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controllerToolStripMenuItem,
            this.endEffectorToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(434, 24);
            this.Menu.TabIndex = 2;
            this.Menu.Text = "menuStrip1";
            // 
            // controllerToolStripMenuItem
            // 
            this.controllerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSBPortToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.controllerToolStripMenuItem.Name = "controllerToolStripMenuItem";
            this.controllerToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.controllerToolStripMenuItem.Text = "Controller";
            // 
            // uSBPortToolStripMenuItem
            // 
            this.uSBPortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSB1ToolStripMenuItem,
            this.uSB2ToolStripMenuItem,
            this.uSB3ToolStripMenuItem,
            this.uSB4ToolStripMenuItem});
            this.uSBPortToolStripMenuItem.Name = "uSBPortToolStripMenuItem";
            this.uSBPortToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.uSBPortToolStripMenuItem.Text = "USB-Port";
            // 
            // uSB1ToolStripMenuItem
            // 
            this.uSB1ToolStripMenuItem.Name = "uSB1ToolStripMenuItem";
            this.uSB1ToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.uSB1ToolStripMenuItem.Text = "USB1";
            // 
            // uSB2ToolStripMenuItem
            // 
            this.uSB2ToolStripMenuItem.Name = "uSB2ToolStripMenuItem";
            this.uSB2ToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.uSB2ToolStripMenuItem.Text = "USB2";
            // 
            // uSB3ToolStripMenuItem
            // 
            this.uSB3ToolStripMenuItem.Name = "uSB3ToolStripMenuItem";
            this.uSB3ToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.uSB3ToolStripMenuItem.Text = "USB3";
            // 
            // uSB4ToolStripMenuItem
            // 
            this.uSB4ToolStripMenuItem.Name = "uSB4ToolStripMenuItem";
            this.uSB4ToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.uSB4ToolStripMenuItem.Text = "USB4";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            // 
            // endEffectorToolStripMenuItem
            // 
            this.endEffectorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComPortList});
            this.endEffectorToolStripMenuItem.Name = "endEffectorToolStripMenuItem";
            this.endEffectorToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.endEffectorToolStripMenuItem.Text = "End-Effector";
            // 
            // ComPortList
            // 
            this.ComPortList.Name = "ComPortList";
            this.ComPortList.Size = new System.Drawing.Size(96, 22);
            this.ComPortList.Text = "Port";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(100, 180);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(225, 45);
            this.trackBar1.TabIndex = 3;
            // 
            // LeftPos
            // 
            this.LeftPos.BackColor = System.Drawing.SystemColors.ControlDark;
            this.LeftPos.Cursor = System.Windows.Forms.Cursors.Cross;
            this.LeftPos.Location = new System.Drawing.Point(19, 231);
            this.LeftPos.Maximum = 180;
            this.LeftPos.Name = "LeftPos";
            this.LeftPos.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.LeftPos.Size = new System.Drawing.Size(45, 144);
            this.LeftPos.TabIndex = 4;
            this.LeftPos.TickFrequency = 30;
            this.LeftPos.Scroll += new System.EventHandler(this.LeftPos_Scroll);
            // 
            // RightPos
            // 
            this.RightPos.BackColor = System.Drawing.SystemColors.ControlDark;
            this.RightPos.Location = new System.Drawing.Point(280, 231);
            this.RightPos.Maximum = 180;
            this.RightPos.Name = "RightPos";
            this.RightPos.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.RightPos.Size = new System.Drawing.Size(45, 144);
            this.RightPos.TabIndex = 5;
            this.RightPos.TickFrequency = 30;
            this.RightPos.Scroll += new System.EventHandler(this.RightPos_Scroll);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(410, 130);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Log);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(402, 104);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Log
            // 
            this.Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log.HideSelection = false;
            this.Log.Location = new System.Drawing.Point(3, 3);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(396, 98);
            this.Log.TabIndex = 0;
            this.Log.UseCompatibleStateImageBehavior = false;
            // 
            // LeftRumble
            // 
            this.LeftRumble.Location = new System.Drawing.Point(100, 231);
            this.LeftRumble.Maximum = 100;
            this.LeftRumble.Name = "LeftRumble";
            this.LeftRumble.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.LeftRumble.Size = new System.Drawing.Size(45, 144);
            this.LeftRumble.TabIndex = 7;
            this.LeftRumble.TickFrequency = 10;
            this.LeftRumble.Scroll += new System.EventHandler(this.LeftRumble_Scroll);
            this.LeftRumble.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftRumble_MouseDown);
            // 
            // RightRumble
            // 
            this.RightRumble.Location = new System.Drawing.Point(377, 231);
            this.RightRumble.Maximum = 100;
            this.RightRumble.Name = "RightRumble";
            this.RightRumble.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.RightRumble.Size = new System.Drawing.Size(45, 144);
            this.RightRumble.TabIndex = 8;
            this.RightRumble.TickFrequency = 10;
            this.RightRumble.Scroll += new System.EventHandler(this.RightRumble_Scroll);
            this.RightRumble.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightRumble_MouseDown);
            // 
            // LeftPosLive
            // 
            this.LeftPosLive.Cursor = System.Windows.Forms.Cursors.Cross;
            this.LeftPosLive.Enabled = false;
            this.LeftPosLive.Location = new System.Drawing.Point(60, 231);
            this.LeftPosLive.Maximum = 100;
            this.LeftPosLive.Name = "LeftPosLive";
            this.LeftPosLive.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.LeftPosLive.Size = new System.Drawing.Size(45, 144);
            this.LeftPosLive.TabIndex = 9;
            this.LeftPosLive.TickFrequency = 10;
            // 
            // RightPosLive
            // 
            this.RightPosLive.Enabled = false;
            this.RightPosLive.Location = new System.Drawing.Point(326, 231);
            this.RightPosLive.Maximum = 100;
            this.RightPosLive.Name = "RightPosLive";
            this.RightPosLive.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.RightPosLive.Size = new System.Drawing.Size(45, 144);
            this.RightPosLive.TabIndex = 10;
            this.RightPosLive.TickFrequency = 10;
            // 
            // X
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 404);
            this.Controls.Add(this.RightPosLive);
            this.Controls.Add(this.LeftPosLive);
            this.Controls.Add(this.RightRumble);
            this.Controls.Add(this.LeftRumble);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.RightPos);
            this.Controls.Add(this.LeftPos);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.RightShoulder);
            this.Controls.Add(this.LeftShoulder);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "X";
            this.Text = "Bomkoffer.nl | Robot controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftRumble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightRumble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPosLive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPosLive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LeftShoulder;
        private System.Windows.Forms.Button RightShoulder;
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem controllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSBPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endEffectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar LeftPos;
        private System.Windows.Forms.TrackBar RightPos;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem uSB1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSB2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSB3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSB4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ComPortList;
        private System.Windows.Forms.TrackBar RightRumble;
        private System.Windows.Forms.TrackBar LeftRumble;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView Log;
        private System.Windows.Forms.TrackBar RightPosLive;
        private System.Windows.Forms.TrackBar LeftPosLive;
        private System.IO.Ports.SerialPort Arduino;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

