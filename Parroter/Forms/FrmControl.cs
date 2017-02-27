using System;
using System.Threading;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using Parroter.Extensions;
using Parroter.Parrot;
using Timer = System.Threading.Timer;

namespace Parroter.Forms
{
    public partial class FrmControl : Form
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
        }

        private async void FrmControl_Load(object sender, EventArgs e)
        {
            Text = $@"Parroter: {Parrot.Device.DeviceName}";

            await Parrot.ConnectAsync();
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

        private async void RefreshTrayIcon(object state)
        {
            var battery = await Parrot.GetBatteryPercentAsync();
            var noiseControl = await Parrot.GetNoiseControlEnabledAsync();

            // Set
            this.InvokeIfRequired(() =>
            {
                TrayIconBatteryText.Text = $"Battery: {battery}%";
                TrayIconNoiseControl.Checked = noiseControl;
            });

            // Refresh after 5 seconds
            RefreshTimer.Change(5000, Timeout.Infinite);
        }

        private async void TrayIconNoiseControl_Click(object sender, EventArgs e)
        {
            var currentState = TrayIconNoiseControl.Checked;

            // Send SET message to parrot
            await Parrot.SetNoiseControlEnabledAsync(!currentState);

            // Change tray
            this.InvokeIfRequired(() =>
            {
                TrayIconNoiseControl.Checked = !currentState;
            });
        }
    }
}
