using BatteryCheck.Models;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace BatteryCheck.Bluetooth;

internal sealed class BluetoothDeviceScanner
{
    private readonly List<IBatteryReader> _batteryReaders =
    [
        new StandardBleBatteryReader()
    ];

    public async Task<List<DeviceInfo>> GetDevicesAsync()
    {
        var byAddress = new Dictionary<ulong, DeviceInfo>();

        await AddBleDevicesAsync(byAddress);
        await AddClassicDevicesAsync(byAddress);

        var devices = byAddress.Values
            .OrderByDescending(d => d.Connected)
            .ThenBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        MakeUniqueNames(devices);
        return devices;
    }

    private async Task AddBleDevicesAsync(Dictionary<ulong, DeviceInfo> byAddress)
    {
        try
        {
            string selector = BluetoothLEDevice.GetDeviceSelectorFromPairingState(true);
            var deviceInfos = await DeviceInformation.FindAllAsync(selector);

            foreach (var deviceInfo in deviceInfos)
            {
                BluetoothLEDevice? bt = null;
                try
                {
                    bt = await BluetoothLEDevice.FromIdAsync(deviceInfo.Id);
                    if (bt is null || bt.BluetoothAddress == 0)
                        continue;

                    bool connected = bt.ConnectionStatus == BluetoothConnectionStatus.Connected;
                    int battery = connected ? await ReadBatteryLevelAsync(bt.BluetoothAddress) : -1;

                    MergeDevice(byAddress, new DeviceInfo
                    {
                        Name = GetDisplayName(bt.Name, deviceInfo.Name),
                        Address = bt.BluetoothAddress,
                        Connected = connected,
                        Battery = battery,
                        LastUpdated = DateTime.Now
                    });
                }
                catch
                {
                    // A stale Windows device entry can fail to open. Skip it.
                }
                finally
                {
                    bt?.Dispose();
                }
            }
        }
        catch
        {
            // BLE radio may be unavailable. Classic devices can still be listed.
        }
    }

    private static async Task AddClassicDevicesAsync(Dictionary<ulong, DeviceInfo> byAddress)
    {
        try
        {
            string selector = BluetoothDevice.GetDeviceSelectorFromPairingState(true);
            var deviceInfos = await DeviceInformation.FindAllAsync(selector);

            foreach (var deviceInfo in deviceInfos)
            {
                BluetoothDevice? bt = null;
                try
                {
                    bt = await BluetoothDevice.FromIdAsync(deviceInfo.Id);
                    if (bt is null || bt.BluetoothAddress == 0)
                        continue;

                    MergeDevice(byAddress, new DeviceInfo
                    {
                        Name = GetDisplayName(bt.Name, deviceInfo.Name),
                        Address = bt.BluetoothAddress,
                        Connected = bt.ConnectionStatus == BluetoothConnectionStatus.Connected,
                        Battery = -1,
                        LastUpdated = DateTime.Now
                    });
                }
                catch
                {
                    // Ignore device entries Windows can no longer open.
                }
                finally
                {
                    bt?.Dispose();
                }
            }
        }
        catch
        {
            // Classic Bluetooth radio may be unavailable.
        }
    }

    private async Task<int> ReadBatteryLevelAsync(ulong bluetoothAddress)
    {
        foreach (var reader in _batteryReaders)
        {
            int battery = await reader.ReadBatteryLevelAsync(bluetoothAddress);
            if (battery >= 0)
                return battery;
        }

        return -1;
    }

    private static void MergeDevice(Dictionary<ulong, DeviceInfo> byAddress, DeviceInfo candidate)
    {
        if (!byAddress.TryGetValue(candidate.Address, out var existing))
        {
            byAddress[candidate.Address] = candidate;
            return;
        }

        if (candidate.Connected)
            existing.Connected = true;

        if (candidate.Battery >= 0)
            existing.Battery = candidate.Battery;

        if ((existing.Name == "(Unknown)" || string.IsNullOrWhiteSpace(existing.Name)) &&
            !string.IsNullOrWhiteSpace(candidate.Name))
            existing.Name = candidate.Name;
    }

    private static string GetDisplayName(string? preferred, string? fallback)
    {
        if (!string.IsNullOrWhiteSpace(preferred))
            return preferred;
        if (!string.IsNullOrWhiteSpace(fallback))
            return fallback;
        return "(Unknown)";
    }

    private static void MakeUniqueNames(List<DeviceInfo> devices)
    {
        var counts = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

        foreach (var device in devices)
        {
            counts.TryGetValue(device.Name, out int count);
            count++;
            counts[device.Name] = count;
            if (count > 1)
                device.Name = $"{device.Name} ({count})";
        }
    }
}
