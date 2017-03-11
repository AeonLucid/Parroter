namespace Parroter.Forms
{
    partial class FrmControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControl));
            this.TrayIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TrayIconBatteryText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TrayIconNoiseControl = new System.Windows.Forms.ToolStripMenuItem();
            this.NoiseControlMaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoiseControlOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoiseControlOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StreetModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StreetModeMaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIconConcertHall = new System.Windows.Forms.ToolStripMenuItem();
            this.RoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConcertRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JazzRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LivingRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SilentRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AngleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Angle30ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Angle60ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Angle90ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Angle120ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Angle150ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Angle180ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayIconContextMenu
            // 
            this.TrayIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TrayIconBatteryText,
            this.toolStripSeparator1,
            this.TrayIconNoiseControl,
            this.TrayIconConcertHall});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new System.Drawing.Size(174, 76);
            // 
            // TrayIconBatteryText
            // 
            this.TrayIconBatteryText.Name = "TrayIconBatteryText";
            this.TrayIconBatteryText.Size = new System.Drawing.Size(173, 22);
            this.TrayIconBatteryText.Text = "Battery: 0%";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // TrayIconNoiseControl
            // 
            this.TrayIconNoiseControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NoiseControlMaxToolStripMenuItem,
            this.NoiseControlOnToolStripMenuItem,
            this.NoiseControlOffToolStripMenuItem,
            this.StreetModeToolStripMenuItem,
            this.StreetModeMaxToolStripMenuItem});
            this.TrayIconNoiseControl.Name = "TrayIconNoiseControl";
            this.TrayIconNoiseControl.Size = new System.Drawing.Size(173, 22);
            this.TrayIconNoiseControl.Text = "Noise Cancellation";
            this.TrayIconNoiseControl.Click += new System.EventHandler(this.TrayIconNoiseControl_Click);
            // 
            // NoiseControlMaxToolStripMenuItem
            // 
            this.NoiseControlMaxToolStripMenuItem.Name = "NoiseControlMaxToolStripMenuItem";
            this.NoiseControlMaxToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.NoiseControlMaxToolStripMenuItem.Text = "Noise control max";
            this.NoiseControlMaxToolStripMenuItem.Click += new System.EventHandler(this.NoiseControlMaxToolStripMenuItem_Click);
            // 
            // NoiseControlOnToolStripMenuItem
            // 
            this.NoiseControlOnToolStripMenuItem.Name = "NoiseControlOnToolStripMenuItem";
            this.NoiseControlOnToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.NoiseControlOnToolStripMenuItem.Text = "Noise control on";
            this.NoiseControlOnToolStripMenuItem.Click += new System.EventHandler(this.NoiseControlOnToolStripMenuItem_Click);
            // 
            // NoiseControlOffToolStripMenuItem
            // 
            this.NoiseControlOffToolStripMenuItem.Name = "NoiseControlOffToolStripMenuItem";
            this.NoiseControlOffToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.NoiseControlOffToolStripMenuItem.Text = "Noise control off";
            this.NoiseControlOffToolStripMenuItem.Click += new System.EventHandler(this.NoiseControlOffToolStripMenuItem_Click);
            // 
            // StreetModeToolStripMenuItem
            // 
            this.StreetModeToolStripMenuItem.Name = "StreetModeToolStripMenuItem";
            this.StreetModeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.StreetModeToolStripMenuItem.Text = "Street mode";
            this.StreetModeToolStripMenuItem.Click += new System.EventHandler(this.StreetModeToolStripMenuItem_Click);
            // 
            // StreetModeMaxToolStripMenuItem
            // 
            this.StreetModeMaxToolStripMenuItem.Name = "StreetModeMaxToolStripMenuItem";
            this.StreetModeMaxToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.StreetModeMaxToolStripMenuItem.Text = "Street mode max";
            this.StreetModeMaxToolStripMenuItem.Click += new System.EventHandler(this.StreetModeMaxToolStripMenuItem_Click);
            // 
            // TrayIconConcertHall
            // 
            this.TrayIconConcertHall.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RoomToolStripMenuItem,
            this.AngleToolStripMenuItem});
            this.TrayIconConcertHall.Name = "TrayIconConcertHall";
            this.TrayIconConcertHall.Size = new System.Drawing.Size(173, 22);
            this.TrayIconConcertHall.Text = "Concert Hall";
            this.TrayIconConcertHall.Click += new System.EventHandler(this.TrayIconConcertHall_Click);
            // 
            // RoomToolStripMenuItem
            // 
            this.RoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConcertRoomToolStripMenuItem,
            this.JazzRoomToolStripMenuItem,
            this.LivingRoomToolStripMenuItem,
            this.SilentRoomToolStripMenuItem});
            this.RoomToolStripMenuItem.Name = "RoomToolStripMenuItem";
            this.RoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.RoomToolStripMenuItem.Text = "Room";
            // 
            // ConcertRoomToolStripMenuItem
            // 
            this.ConcertRoomToolStripMenuItem.Name = "ConcertRoomToolStripMenuItem";
            this.ConcertRoomToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ConcertRoomToolStripMenuItem.Text = "Concert";
            this.ConcertRoomToolStripMenuItem.Click += new System.EventHandler(this.ConcertRoomToolStripMenuItem_Click);
            // 
            // JazzRoomToolStripMenuItem
            // 
            this.JazzRoomToolStripMenuItem.Name = "JazzRoomToolStripMenuItem";
            this.JazzRoomToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.JazzRoomToolStripMenuItem.Text = "Jazz";
            this.JazzRoomToolStripMenuItem.Click += new System.EventHandler(this.JazzRoomToolStripMenuItem_Click);
            // 
            // LivingRoomToolStripMenuItem
            // 
            this.LivingRoomToolStripMenuItem.Name = "LivingRoomToolStripMenuItem";
            this.LivingRoomToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.LivingRoomToolStripMenuItem.Text = "Living";
            this.LivingRoomToolStripMenuItem.Click += new System.EventHandler(this.LivingRoomToolStripMenuItem_Click);
            // 
            // SilentRoomToolStripMenuItem
            // 
            this.SilentRoomToolStripMenuItem.Name = "SilentRoomToolStripMenuItem";
            this.SilentRoomToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.SilentRoomToolStripMenuItem.Text = "Silent";
            this.SilentRoomToolStripMenuItem.Click += new System.EventHandler(this.SilentRoomToolStripMenuItem_Click);
            // 
            // AngleToolStripMenuItem
            // 
            this.AngleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Angle30ToolStripMenuItem,
            this.Angle60ToolStripMenuItem,
            this.Angle90ToolStripMenuItem,
            this.Angle120ToolStripMenuItem,
            this.Angle150ToolStripMenuItem,
            this.Angle180ToolStripMenuItem});
            this.AngleToolStripMenuItem.Name = "AngleToolStripMenuItem";
            this.AngleToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.AngleToolStripMenuItem.Text = "Angle";
            // 
            // Angle30ToolStripMenuItem
            // 
            this.Angle30ToolStripMenuItem.Name = "Angle30ToolStripMenuItem";
            this.Angle30ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.Angle30ToolStripMenuItem.Text = "30";
            this.Angle30ToolStripMenuItem.Click += new System.EventHandler(this.Angle30ToolStripMenuItem_Click);
            // 
            // Angle60ToolStripMenuItem
            // 
            this.Angle60ToolStripMenuItem.Name = "Angle60ToolStripMenuItem";
            this.Angle60ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.Angle60ToolStripMenuItem.Text = "60";
            this.Angle60ToolStripMenuItem.Click += new System.EventHandler(this.Angle60ToolStripMenuItem_Click);
            // 
            // Angle90ToolStripMenuItem
            // 
            this.Angle90ToolStripMenuItem.Name = "Angle90ToolStripMenuItem";
            this.Angle90ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.Angle90ToolStripMenuItem.Text = "90";
            this.Angle90ToolStripMenuItem.Click += new System.EventHandler(this.Angle90ToolStripMenuItem_Click);
            // 
            // Angle120ToolStripMenuItem
            // 
            this.Angle120ToolStripMenuItem.Name = "Angle120ToolStripMenuItem";
            this.Angle120ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.Angle120ToolStripMenuItem.Text = "120";
            this.Angle120ToolStripMenuItem.Click += new System.EventHandler(this.Angle120ToolStripMenuItem_Click);
            // 
            // Angle150ToolStripMenuItem
            // 
            this.Angle150ToolStripMenuItem.Name = "Angle150ToolStripMenuItem";
            this.Angle150ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.Angle150ToolStripMenuItem.Text = "150";
            this.Angle150ToolStripMenuItem.Click += new System.EventHandler(this.Angle150ToolStripMenuItem_Click);
            // 
            // Angle180ToolStripMenuItem
            // 
            this.Angle180ToolStripMenuItem.Name = "Angle180ToolStripMenuItem";
            this.Angle180ToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.Angle180ToolStripMenuItem.Text = "180";
            this.Angle180ToolStripMenuItem.Click += new System.EventHandler(this.Angle180ToolStripMenuItem_Click);
            // 
            // FrmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 335);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parroter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmControl_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmControl_FormClosed);
            this.Load += new System.EventHandler(this.FrmControl_Load);
            this.TrayIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip TrayIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem TrayIconBatteryText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TrayIconNoiseControl;
        private System.Windows.Forms.ToolStripMenuItem NoiseControlMaxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoiseControlOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoiseControlOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StreetModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StreetModeMaxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TrayIconConcertHall;
        private System.Windows.Forms.ToolStripMenuItem RoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConcertRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem JazzRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LivingRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SilentRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AngleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Angle30ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Angle60ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Angle90ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Angle120ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Angle150ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Angle180ToolStripMenuItem;
    }
}