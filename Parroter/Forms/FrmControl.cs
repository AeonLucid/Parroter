using System;
using System.Threading;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using Parroter.Extensions;
using Parroter.Parrot;
using Parroter.Parrot.Controls.Noise;
using Timer = System.Threading.Timer;

namespace Parroter.Forms
{
    internal partial class FrmControl : Form
    {
        private Timer RefreshTimer { get; }

        private NotifyIcon TrayIcon { get; }

        private ParrotClient Parrot { get; }

        public FrmControl(NotifyIcon trayIcon, BluetoothDeviceInfo device)
        {
            InitializeComponent();

            RefreshTimer = new Timer(RefreshTrayIcon, null, Timeout.Infinite, Timeout.Infinite);
            TrayIcon = trayIcon;
            Parrot = new ParrotClient(device);
            Parrot.ConnectedEvent += ParrotOnConnectedEvent;
            Parrot.Battery.ChangedEvent += BatteryOnChangedEvent;
            Parrot.NoiseControl.ChangedEvent += NoiseControlOnChangedEvent;
        }

        private async void FrmControl_Load(object sender, EventArgs e)
        {
            Text = $@"Parroter: {Parrot.Device.DeviceName}";

            await Parrot.ConnectAsync();
        }

        private void FrmControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void FrmControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ParrotOnConnectedEvent(object sender, EventArgs eventArgs)
        {
            this.InvokeIfRequired(() =>
            {
                TrayIcon.ContextMenuStrip = TrayIconContextMenu;
                TrayIcon.Text = $"Parroter\r\nConnected to {Parrot.Device.DeviceName}";
                TrayIcon.ShowBalloonTip(5000, "Parroter connection was successful.", $"Parroter has been succesfully connected to your {Parrot.Device.DeviceName}.", ToolTipIcon.Info);
            });

            RefreshTimer.Change(5000, Timeout.Infinite);
        }

        private async void RefreshTrayIcon(object state)
        {
            await Parrot.Battery.RefreshAsync();
            
            RefreshTimer.Change(5000, Timeout.Infinite);
        }

        #region Battery control
        private void BatteryOnChangedEvent(object sender, EventArgs eventArgs)
        {
            this.InvokeIfRequired(() =>
            {
                TrayIconBatteryText.Text = $"Battery: {Parrot.Battery.Percent}%";

                if (Parrot.Battery.Charging)
                {
                    TrayIconBatteryText.Image = Properties.Resources.BatteryCharging;
                }
                else if (Parrot.Battery.Percent >= 75)
                {
                    TrayIconBatteryText.Image = Properties.Resources.BatteryFull;
                }
                else if (Parrot.Battery.Percent >= 25)
                {
                    TrayIconBatteryText.Image = Properties.Resources.BatteryHalf;
                }
                else
                {
                    TrayIconBatteryText.Image = Properties.Resources.BatteryLow;
                }
            });
        }
        #endregion

        #region Noise control management
        private async void TrayIconNoiseControl_Click(object sender, EventArgs e)
        {
            await Parrot.NoiseControl.SetEnabledAsync(!Parrot.NoiseControl.Enabled);
        }

        private void NoiseControlOnChangedEvent(object sender, EventArgs eventArgs)
        {
            this.InvokeIfRequired(() =>
            {
                TrayIconNoiseControl.Checked = Parrot.NoiseControl.Enabled;

                NoiseControlMaxToolStripMenuItem.Checked = Parrot.NoiseControl.Type == NoiseControlType.NoiseControlMax;
                NoiseControlOnToolStripMenuItem.Checked = Parrot.NoiseControl.Type == NoiseControlType.NoiseControlOn;
                NoiseControlOffToolStripMenuItem.Checked = Parrot.NoiseControl.Type == NoiseControlType.NoiseControlOff;
                StreetModeToolStripMenuItem.Checked = Parrot.NoiseControl.Type == NoiseControlType.StreetMode;
                StreetModeMaxToolStripMenuItem.Checked = Parrot.NoiseControl.Type == NoiseControlType.StreetModeMax;
            });
        }

        private async void NoiseControlMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Parrot.NoiseControl.SetTypeAsync(NoiseControlType.NoiseControlMax);
        }

        private async void NoiseControlOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Parrot.NoiseControl.SetTypeAsync(NoiseControlType.NoiseControlOn);
        }

        private async void NoiseControlOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Parrot.NoiseControl.SetTypeAsync(NoiseControlType.NoiseControlOff);
        }

        private async void StreetModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Parrot.NoiseControl.SetTypeAsync(NoiseControlType.StreetMode);
        }

        private async void StreetModeMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Parrot.NoiseControl.SetTypeAsync(NoiseControlType.StreetModeMax);
        }
        #endregion
    }
}
