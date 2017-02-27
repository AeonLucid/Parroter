using System;
using System.Threading;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using Parroter.Extensions;
using Parroter.Parrot;
using Timer = System.Threading.Timer;

namespace Parroter.Forms
{
    internal partial class FrmControl : Form
    {
        public Timer RefreshTimer { get; }

        public NotifyIcon TrayIcon { get; }

        public ParrotClient Parrot { get; }

        public FrmControl(NotifyIcon trayIcon, BluetoothDeviceInfo device)
        {
            InitializeComponent();

            RefreshTimer = new Timer(RefreshTrayIcon, null, Timeout.Infinite, Timeout.Infinite);
            TrayIcon = trayIcon;
            Parrot = new ParrotClient(device);
            Parrot.ConnectedEvent += ParrotOnConnectedEvent;
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
            RefreshTrayIcon(null);

            this.InvokeIfRequired(() =>
            {
                TrayIcon.ContextMenuStrip = TrayIconContextMenu;
                TrayIcon.Text = $"Parroter\r\nConnected to {Parrot.Device.DeviceName}";
                TrayIcon.ShowBalloonTip(5000, "Parroter connection was successful.", $"Parroter has been succesfully connected to your {Parrot.Device.DeviceName}.", ToolTipIcon.Info);
            });
        }
        
        private void NoiseControlOnChangedEvent(object sender, EventArgs eventArgs)
        {
            this.InvokeIfRequired(() =>
            {
                TrayIconNoiseControl.Checked = Parrot.NoiseControl.Enabled;
            });
        }

        private async void RefreshTrayIcon(object state)
        {
            var battery = await Parrot.GetBatteryPercentAsync();

            // Update tray
            this.InvokeIfRequired(() =>
            {
                TrayIconBatteryText.Text = $"Battery: {battery}%";
            });

            // Refresh after 5 seconds
            RefreshTimer.Change(5000, Timeout.Infinite);
        }

        private async void TrayIconNoiseControl_Click(object sender, EventArgs e)
        {
            // Toggle
            await Parrot.NoiseControl.SetEnabledAsync(!Parrot.NoiseControl.Enabled);
        }
    }
}
