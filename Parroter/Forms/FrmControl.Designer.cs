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
            this.TrayIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayIconContextMenu
            // 
            this.TrayIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TrayIconBatteryText,
            this.toolStripSeparator1,
            this.TrayIconNoiseControl});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new System.Drawing.Size(146, 54);
            // 
            // TrayIconBatteryText
            // 
            this.TrayIconBatteryText.Name = "TrayIconBatteryText";
            this.TrayIconBatteryText.Size = new System.Drawing.Size(145, 22);
            this.TrayIconBatteryText.Text = "Battery: 0%";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // TrayIconNoiseControl
            // 
            this.TrayIconNoiseControl.Name = "TrayIconNoiseControl";
            this.TrayIconNoiseControl.Size = new System.Drawing.Size(145, 22);
            this.TrayIconNoiseControl.Text = "Noise control";
            this.TrayIconNoiseControl.Click += new System.EventHandler(this.TrayIconNoiseControl_Click);
            // 
            // FrmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 455);
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
    }
}