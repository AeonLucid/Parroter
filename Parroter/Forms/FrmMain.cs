using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Timer = System.Threading.Timer;

namespace Parroter.Forms
{
    public partial class FrmMain : Form
    {
        private readonly string[] _validParrots = {
            "Parrot ZIK 3 V3.07"
        };

        private readonly BluetoothClient _bluetoothClient;
        private readonly Timer _devicesTimer;

        public FrmMain()
        {
            InitializeComponent();

            if (!BluetoothRadio.IsSupported)
            {
                MessageBox.Show(@"No valid bluetooth stack was found, please try again.", @"Woops..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            _bluetoothClient = new BluetoothClient();
            _devicesTimer = new Timer(CheckDevices, null, 200, Timeout.Infinite);
        }

        private void CheckDevices(object state)
        {
            var devices = _bluetoothClient.DiscoverDevices(5);
            var device = devices.FirstOrDefault(btDevice => _validParrots.Contains(btDevice.DeviceName));
            if (device != null)
            {
                if (!device.Authenticated)
                {
                    Invoke(new Action(() =>
                    {
                        LabelStatus.Text = @"The Parrot ZIK 3 we found is not authenticated";
                        LabelStatus.Refresh();
                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        // Show the control form.
                        new FrmControl(TrayIcon, device).Show();

                        // Close the connect form.
                        Hide();
                    }));

                    // Stop the timer.
                    _devicesTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    return;
                }
            }

            // Reset timer.
            _devicesTimer.Change(1000, Timeout.Infinite);
        }
    }
}
