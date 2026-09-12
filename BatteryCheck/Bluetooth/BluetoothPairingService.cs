using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace BatteryCheck.Bluetooth;

internal sealed class BluetoothPairingService
{
	private static readonly TimeSpan ScanDuration = TimeSpan.FromSeconds(12);

	public async Task<List<DeviceInformation>> DiscoverUnpairedDevicesAsync()
	{
		var bleTask = WatchForDevicesAsync(
			BluetoothLEDevice.GetDeviceSelectorFromPairingState(false));

		var classicTask = WatchForDevicesAsync(
			BluetoothDevice.GetDeviceSelectorFromPairingState(false));

		var groups = await Task.WhenAll(bleTask, classicTask);

		var resultsById = new Dictionary<string, DeviceInformation>(
			StringComparer.OrdinalIgnoreCase);

		foreach (var group in groups)
		{
			foreach (var device in group)
			{
				if (string.IsNullOrWhiteSpace(device.Name))
					continue;

				if (device.Pairing.IsPaired)
					continue;

				resultsById[device.Id] = device;
			}
		}

		var uniqueByName = new Dictionary<string, DeviceInformation>(
			StringComparer.CurrentCultureIgnoreCase);

		foreach (var device in resultsById.Values)
		{
			string normalizedName = device.Name.Trim();

			if (!uniqueByName.ContainsKey(normalizedName))
			{
				uniqueByName[normalizedName] = device;
			}
		}

		return uniqueByName.Values
			.OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase)
			.ToList();
	}

	private static async Task<List<DeviceInformation>> WatchForDevicesAsync(
		string selector)
	{
		var results = new Dictionary<string, DeviceInformation>(
			StringComparer.OrdinalIgnoreCase);

		DeviceWatcher? watcher = null;

		try
		{
			watcher = DeviceInformation.CreateWatcher(selector);

			watcher.Added += (_, device) =>
			{
				lock (results)
				{
					results[device.Id] = device;
				}
			};

			watcher.Updated += (_, update) =>
			{
				lock (results)
				{
					if (results.TryGetValue(update.Id, out var existing))
					{
						existing.Update(update);
					}
				}
			};

			watcher.Removed += (_, update) =>
			{
				lock (results)
				{
					results.Remove(update.Id);
				}
			};

			watcher.Start();

			await Task.Delay(ScanDuration);
		}
		catch
		{
			// One Bluetooth radio type may be unavailable.
		}
		finally
		{
			if (watcher is not null &&
				watcher.Status is DeviceWatcherStatus.Started
					or DeviceWatcherStatus.EnumerationCompleted)
			{
				watcher.Stop();
			}
		}

		lock (results)
		{
			return results.Values.ToList();
		}
	}

	public async Task<DevicePairingResult?> PairAsync(DeviceInformation device)
	{
		try
		{
			if (device.Pairing.IsPaired)
				return null;

			if (!device.Pairing.CanPair)
				return null;

			return await device.Pairing.PairAsync(
				DevicePairingProtectionLevel.Default);
		}
		catch
		{
			return null;
		}
	}

	public async Task<bool> UnpairAsync(DeviceInformation device)
	{
		try
		{
			if (!device.Pairing.IsPaired)
				return true;

			var result = await device.Pairing.UnpairAsync();

			return result.Status == DeviceUnpairingResultStatus.Unpaired;
		}
		catch
		{
			return false;
		}
	}
}