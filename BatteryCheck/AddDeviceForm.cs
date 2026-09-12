using BatteryCheck.Bluetooth;
using Windows.Devices.Enumeration;

namespace BatteryCheck;

internal sealed class AddDeviceForm : Form
{
    private readonly BluetoothPairingService _pairingService = new();
    private readonly ListBox _devices = new() { Dock = DockStyle.Fill, IntegralHeight = false };
    private readonly Button _scanButton = new() { Text = "Scan again", AutoSize = true };
    private readonly Button _addButton = new() { Text = "Add / Pair", AutoSize = true, Enabled = false };
    private readonly Label _status = new() { AutoSize = true, Text = "Ready to scan." };

    public AddDeviceForm()
    {
        Text = "Add Bluetooth Device";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.Sizable;
        MinimumSize = new Size(430, 320);
        ClientSize = new Size(500, 380);
        ShowIcon = false;
        ShowInTaskbar = false;

        var top = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 48,
            Padding = new Padding(8),
            FlowDirection = FlowDirection.LeftToRight
        };
        top.Controls.Add(_scanButton);
        top.Controls.Add(_addButton);

		var bottom = new System.Windows.Forms.Panel { Dock = DockStyle.Bottom, Height = 42, Padding = new Padding(10, 8, 8, 8) }; bottom.Controls.Add(_status);

        Controls.Add(_devices);
        Controls.Add(bottom);
        Controls.Add(top);

        _devices.SelectedIndexChanged += (_, _) => _addButton.Enabled = _devices.SelectedItem is DeviceItem;
        _devices.DoubleClick += async (_, _) => await PairSelectedAsync();
        _scanButton.Click += async (_, _) => await ScanAsync();
        _addButton.Click += async (_, _) => await PairSelectedAsync();
        Shown += async (_, _) => await ScanAsync();
    }

    private async Task ScanAsync()
    {
        SetBusy(true, "Scanning for nearby Bluetooth devices…");
        _devices.Items.Clear();

        try
        {
            var devices = await _pairingService.DiscoverUnpairedDevicesAsync();
            foreach (var device in devices)
                _devices.Items.Add(new DeviceItem(device));

            _status.Text = devices.Count == 0
                ? "No new devices found. Put the device in pairing mode and scan again."
                : $"Found {devices.Count} device{(devices.Count == 1 ? "" : "s")}. Select one and click Add / Pair.";
        }
        finally
        {
            SetBusy(false, _status.Text);
        }
    }

    private async Task PairSelectedAsync()
    {
        if (_devices.SelectedItem is not DeviceItem item)
            return;

        SetBusy(true, $"Pairing with {item.Device.Name}…");

        DevicePairingResult? result = await _pairingService.PairAsync(item.Device);
        if (result is not null && result.Status == DevicePairingResultStatus.Paired)
        {
            _status.Text = $"{item.Device.Name} was paired successfully.";
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

        if (item.Device.Pairing.IsPaired)
        {
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

        _status.Text = result is null
            ? "Windows could not pair this device. Make sure it is in pairing mode."
            : $"Pairing did not complete: {result.Status}.";
        SetBusy(false, _status.Text);
    }

    private void SetBusy(bool busy, string status)
    {
        UseWaitCursor = busy;
        _scanButton.Enabled = !busy;
        _addButton.Enabled = !busy && _devices.SelectedItem is DeviceItem;
        _devices.Enabled = !busy;
        _status.Text = status;
    }

    private sealed class DeviceItem(DeviceInformation device)
    {
        public DeviceInformation Device { get; } = device;
        public override string ToString() => Device.Name;
    }
}
