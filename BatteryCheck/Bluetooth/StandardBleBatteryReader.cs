using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Storage.Streams;

namespace BatteryCheck.Bluetooth;

public sealed class StandardBleBatteryReader : IBatteryReader
{
    public async Task<int> ReadBatteryLevelAsync(ulong bluetoothAddress)
    {
        BluetoothLEDevice? device = null;
        GattDeviceService? service = null;

        try
        {
            device = await BluetoothLEDevice.FromBluetoothAddressAsync(bluetoothAddress);
            if (device is null || device.ConnectionStatus != BluetoothConnectionStatus.Connected)
                return -1;

            var result = await device.GetGattServicesForUuidAsync(
                GattServiceUuids.Battery,
                BluetoothCacheMode.Cached);

            if (result.Status != GattCommunicationStatus.Success || result.Services.Count == 0)
                return -1;

            service = result.Services[0];
            var characteristicResult = await service.GetCharacteristicsForUuidAsync(
                GattCharacteristicUuids.BatteryLevel,
                BluetoothCacheMode.Cached);

            if (characteristicResult.Status != GattCommunicationStatus.Success ||
                characteristicResult.Characteristics.Count == 0)
                return -1;

            var readResult = await characteristicResult.Characteristics[0].ReadValueAsync(BluetoothCacheMode.Cached);
            if (readResult.Status != GattCommunicationStatus.Success || readResult.Value.Length == 0)
                return -1;

            using var reader = DataReader.FromBuffer(readResult.Value);
            int value = reader.ReadByte();
            return value is >= 0 and <= 100 ? value : -1;
        }
        catch
        {
            return -1;
        }
        finally
        {
            service?.Dispose();
            device?.Dispose();
        }
    }
}
