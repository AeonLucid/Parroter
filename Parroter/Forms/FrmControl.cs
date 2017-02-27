using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
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

        private void FrmControl_Load(object sender, EventArgs e)
        {
            Text = $@"Parroter: {Parrot.Device.DeviceName}";

            Task.Run(delegate { Parrot.Connect(); });
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

        private void RefreshTrayIcon(object state)
        {
            // Request
            var battery = Parrot.SendMessage(new ParrotMessage(ZikApi.BatteryGet));
            var batteryValue = battery.XPathSelectElement("/system/battery").Attribute("percent")?.Value;

            var noiseControl = Parrot.SendMessage(new ParrotMessage(ZikApi.NoiseControlEnabledGet));
            var noiseControlValue = noiseControl.XPathSelectElement("/audio/noise_control").Attribute("enabled")?.Value;

            // Check
            if (batteryValue == null || noiseControlValue == null) 
                throw new Exception("Unable to refresh tray icon.");

            // Set
            this.InvokeIfRequired(() =>
            {
                TrayIconBatteryText.Text = $"Battery: {batteryValue}%";
                TrayIconNoiseControl.Checked = noiseControlValue.Equals("true");
            });

            // Refresh after 5 seconds
            RefreshTimer.Change(5000, Timeout.Infinite);
        }

        private void TrayIconNoiseControl_Click(object sender, EventArgs e)
        {
            var currentState = TrayIconNoiseControl.Checked;

            // Send SET message to parrot
            Parrot.SendMessage(new ParrotMessage(ZikApi.NoiseControlEnabledSet, currentState ? "false" : "true"));

            // Change tray
            this.InvokeIfRequired(() =>
            {
                TrayIconNoiseControl.Checked = !currentState;
            });
        }
    }
}
