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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.controllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endEffectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSBPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uSB1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSB2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSB3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSB4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOM1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOM2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 62);
            this.button1.TabIndex = 0;
            this.button1.Text = "Grab";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(317, 163);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 62);
            this.button2.TabIndex = 1;
            this.button2.Text = "Grab";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controllerToolStripMenuItem,
            this.endEffectorToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
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
            // endEffectorToolStripMenuItem
            // 
            this.endEffectorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cOMToolStripMenuItem});
            this.endEffectorToolStripMenuItem.Name = "endEffectorToolStripMenuItem";
            this.endEffectorToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.endEffectorToolStripMenuItem.Text = "End-Effector";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // uSBPortToolStripMenuItem
            // 
            this.uSBPortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSB1ToolStripMenuItem,
            this.uSB2ToolStripMenuItem,
            this.uSB3ToolStripMenuItem,
            this.uSB4ToolStripMenuItem});
            this.uSBPortToolStripMenuItem.Name = "uSBPortToolStripMenuItem";
            this.uSBPortToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSBPortToolStripMenuItem.Text = "USB-Port";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(100, 277);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(225, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(39, 231);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 144);
            this.trackBar2.TabIndex = 4;
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(347, 231);
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar3.Size = new System.Drawing.Size(45, 144);
            this.trackBar3.TabIndex = 5;
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(402, 104);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // uSB1ToolStripMenuItem
            // 
            this.uSB1ToolStripMenuItem.Name = "uSB1ToolStripMenuItem";
            this.uSB1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSB1ToolStripMenuItem.Text = "USB1";
            // 
            // uSB2ToolStripMenuItem
            // 
            this.uSB2ToolStripMenuItem.Name = "uSB2ToolStripMenuItem";
            this.uSB2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSB2ToolStripMenuItem.Text = "USB2";
            // 
            // uSB3ToolStripMenuItem
            // 
            this.uSB3ToolStripMenuItem.Name = "uSB3ToolStripMenuItem";
            this.uSB3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSB3ToolStripMenuItem.Text = "USB3";
            // 
            // uSB4ToolStripMenuItem
            // 
            this.uSB4ToolStripMenuItem.Name = "uSB4ToolStripMenuItem";
            this.uSB4ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSB4ToolStripMenuItem.Text = "USB4";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            // 
            // cOMToolStripMenuItem
            // 
            this.cOMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cOM1ToolStripMenuItem,
            this.cOM2ToolStripMenuItem});
            this.cOMToolStripMenuItem.Name = "cOMToolStripMenuItem";
            this.cOMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cOMToolStripMenuItem.Text = "COM";
            // 
            // cOM1ToolStripMenuItem
            // 
            this.cOM1ToolStripMenuItem.Name = "cOM1ToolStripMenuItem";
            this.cOM1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cOM1ToolStripMenuItem.Text = "COM1";
            // 
            // cOM2ToolStripMenuItem
            // 
            this.cOM2ToolStripMenuItem.Name = "cOM2ToolStripMenuItem";
            this.cOM2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cOM2ToolStripMenuItem.Text = "COM2";
            // 
            // X
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 404);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "X";
            this.Text = "X";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem controllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSBPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endEffectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem uSB1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSB2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSB3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSB4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOM1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOM2ToolStripMenuItem;
    }
}

