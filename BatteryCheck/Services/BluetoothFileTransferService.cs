using System.Diagnostics;

namespace BatteryCheck.Services;

public sealed class BluetoothFileTransferService
{
	public void OpenBluetoothFileTransfer()
	{
		Process.Start(new ProcessStartInfo
		{
			FileName = "fsquirt.exe",
			UseShellExecute = true
		});
	}
}