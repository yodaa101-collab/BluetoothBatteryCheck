namespace BatteryCheck.Bluetooth
{
	public interface IBatteryReader
	{
		Task<int> ReadBatteryLevelAsync(ulong bluetoothAddress);
	}
}