using System.Globalization;
using BatteryCheck.Bluetooth;
using BatteryCheck.Services;
using BatteryCheck.Settings;
using Windows.Devices.Bluetooth;

namespace BatteryCheck;

public partial class Form1 : Form
{
	private readonly BluetoothDeviceScanner _scanner = new();
	private readonly BluetoothPairingService _pairingService = new();
	private readonly System.Windows.Forms.Timer _refreshTimer = new();

	private AppSettings _settings;
	private bool _isRefreshing;

	public Form1()
	{
		InitializeComponent();

		// Use the icon embedded in BatteryCheck.exe
		try
		{
			var appIcon =
				System.Drawing.Icon.ExtractAssociatedIcon(
					Application.ExecutablePath);

			if (appIcon is not null)
			{
				Icon = appIcon;
			}
		}
		catch
		{
			// If Windows cannot load the icon,
			// BatteryCheck will continue normally.
		}

		_settings = AppSettingsStore.Load();
		_settings.StartWithWindows = StartupManager.IsEnabled();

		ConfigureGrid();
		ApplySettings();

		_refreshTimer.Tick += RefreshTimer_Tick;
		FormClosing += Form1_FormClosing;
	}

	private void ConfigureGrid()
	{
		colAddress.Visible = false;

		gridDevices.AutoSizeColumnsMode =
			DataGridViewAutoSizeColumnsMode.Fill;

		gridDevices.RowHeadersVisible = false;
		gridDevices.AllowUserToAddRows = false;
		gridDevices.AllowUserToDeleteRows = false;
		gridDevices.AllowUserToResizeRows = false;

		gridDevices.ClearSelection();

		btnRemoveDevice.Enabled = false;
	}

	private void ApplySettings()
	{
		_refreshTimer.Interval =
			Math.Clamp(
				_settings.RefreshIntervalSeconds,
				10,
				3600) * 1000;

		if (_settings.RememberWindowSize &&
			_settings.WindowWidth >= MinimumSize.Width &&
			_settings.WindowHeight >= MinimumSize.Height)
		{
			Size = new Size(
				_settings.WindowWidth,
				_settings.WindowHeight);
		}

		if (_settings.RememberWindowPosition &&
			_settings.HasSavedWindowBounds)
		{
			var desired = new Rectangle(
				_settings.WindowX,
				_settings.WindowY,
				Width,
				Height);

			bool visibleOnScreen =
				Screen.AllScreens.Any(
					screen =>
						screen.WorkingArea.IntersectsWith(desired));

			if (visibleOnScreen)
			{
				StartPosition = FormStartPosition.Manual;

				Location = new Point(
					_settings.WindowX,
					_settings.WindowY);
			}
		}
	}

	private async void Form1_Load(
		object sender,
		EventArgs e)
	{
		await RefreshDevicesAsync();

		_refreshTimer.Start();
	}

	private async void btnRefresh_Click(
		object sender,
		EventArgs e)
	{
		await RefreshDevicesAsync();
	}

	private async void RefreshTimer_Tick(
		object? sender,
		EventArgs e)
	{
		await RefreshDevicesAsync();
	}

	private async Task RefreshDevicesAsync()
	{
		if (_isRefreshing)
			return;

		_isRefreshing = true;

		try
		{
			SetScanningState(true);

			gridDevices.Rows.Clear();

			var devices =
				await _scanner.GetDevicesAsync();

			foreach (var device in devices)
			{
				gridDevices.Rows.Add(
					device.Name,
					device.Address.ToString("X12"),
					device.Battery >= 0
						? $"{device.Battery}%"
						: "Unavailable",
					device.Connected
						? "Connected"
						: "Paired");
			}

			gridDevices.ClearSelection();
			btnRemoveDevice.Enabled = false;

			Text = "BatteryCheck";

			lblDeviceCount.Text =
				$"{devices.Count} Bluetooth device" +
				$"{(devices.Count == 1 ? "" : "s")}";

			lblLastUpdated.Text =
				$"Updated {DateTime.Now:T}";

			if (devices.Count == 0)
			{
				lblDeviceCount.Text =
					"No paired Bluetooth devices found";
			}
		}
		catch (Exception ex)
		{
			lblLastUpdated.Text =
				"Refresh failed";

			MessageBox.Show(
				this,
				"BatteryCheck could not refresh the " +
				"Bluetooth device list.\n\n" +
				ex.Message,
				"BatteryCheck",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning);
		}
		finally
		{
			SetScanningState(false);

			_isRefreshing = false;

			btnRemoveDevice.Enabled =
				gridDevices.SelectedRows.Count > 0;
		}
	}

	private void SetScanningState(bool scanning)
	{
		btnRefresh.Enabled = !scanning;
		btnAddDevice.Enabled = !scanning;

		btnRemoveDevice.Enabled =
			!scanning &&
			gridDevices.SelectedRows.Count > 0;

		btnRefresh.Text =
			scanning
				? "Scanning…"
				: "Refresh";

		UseWaitCursor = scanning;
	}

	private async void btnAddDevice_Click(
		object sender,
		EventArgs e)
	{
		using var dialog =
			new AddDeviceForm();

		if (dialog.ShowDialog(this) ==
			DialogResult.OK)
		{
			await RefreshDevicesAsync();
		}
	}

	private void gridDevices_SelectionChanged(
		object sender,
		EventArgs e)
	{
		btnRemoveDevice.Enabled =
			!_isRefreshing &&
			gridDevices.SelectedRows.Count > 0;
	}

	private async void btnRemoveDevice_Click(
		object sender,
		EventArgs e)
	{
		if (gridDevices.SelectedRows.Count == 0)
			return;

		DataGridViewRow row =
			gridDevices.SelectedRows[0];

		string deviceName =
			row.Cells[colDevice.Index].Value?.ToString()
			?? "this device";

		string addressText =
			row.Cells[colAddress.Index].Value?.ToString()
			?? string.Empty;

		var answer = MessageBox.Show(
			this,
			$"Remove \"{deviceName}\" from Windows Bluetooth?\n\n" +
			"The device will be unpaired from this computer. " +
			"You can pair it again later.",
			"Remove Bluetooth Device",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Warning,
			MessageBoxDefaultButton.Button2);

		if (answer != DialogResult.Yes)
			return;

		if (!ulong.TryParse(
				addressText,
				NumberStyles.HexNumber,
				CultureInfo.InvariantCulture,
				out ulong bluetoothAddress))
		{
			MessageBox.Show(
				this,
				"BatteryCheck could not determine the " +
				"Bluetooth address for this device.",
				"BatteryCheck",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning);

			return;
		}

		btnRemoveDevice.Enabled = false;
		btnAddDevice.Enabled = false;
		btnRefresh.Enabled = false;
		gridDevices.Enabled = false;
		UseWaitCursor = true;

		lblLastUpdated.Text =
			$"Removing {deviceName}…";

		try
		{
			bool removed =
				await TryUnpairDeviceAsync(
					bluetoothAddress);

			if (removed)
			{
				MessageBox.Show(
					this,
					$"\"{deviceName}\" was removed successfully.",
					"BatteryCheck",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);

				await RefreshDevicesAsync();
			}
			else
			{
				MessageBox.Show(
					this,
					$"BatteryCheck could not remove " +
					$"\"{deviceName}\".\n\n" +
					"The device may already be disconnected, " +
					"or Windows may not allow this device to " +
					"be unpaired through the app.",
					"BatteryCheck",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				await RefreshDevicesAsync();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				this,
				$"BatteryCheck could not remove " +
				$"\"{deviceName}\".\n\n{ex.Message}",
				"BatteryCheck",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning);
		}
		finally
		{
			gridDevices.Enabled = true;
			btnAddDevice.Enabled = true;
			btnRefresh.Enabled = true;
			UseWaitCursor = false;

			btnRemoveDevice.Enabled =
				gridDevices.SelectedRows.Count > 0;
		}
	}

	private async Task<bool> TryUnpairDeviceAsync(
		ulong bluetoothAddress)
	{
		try
		{
			using BluetoothLEDevice? bleDevice =
				await BluetoothLEDevice
					.FromBluetoothAddressAsync(
						bluetoothAddress);

			if (bleDevice is not null &&
				bleDevice.DeviceInformation
					.Pairing.IsPaired)
			{
				bool removed =
					await _pairingService.UnpairAsync(
						bleDevice.DeviceInformation);

				if (removed)
					return true;
			}
		}
		catch
		{
			// It may not be a BLE device.
			// Try classic Bluetooth next.
		}

		try
		{
			using BluetoothDevice? classicDevice =
				await BluetoothDevice
					.FromBluetoothAddressAsync(
						bluetoothAddress);

			if (classicDevice is not null &&
				classicDevice.DeviceInformation
					.Pairing.IsPaired)
			{
				bool removed =
					await _pairingService.UnpairAsync(
						classicDevice.DeviceInformation);

				if (removed)
					return true;
			}
		}
		catch
		{
			// Windows could not open this device.
		}

		return false;
	}

	private async void mnuFileRefresh_Click(
		object sender,
		EventArgs e)
	{
		await RefreshDevicesAsync();
	}

	private void mnuFileExit_Click(
		object sender,
		EventArgs e)
	{
		Close();
	}

	private void mnuSettingsPreferences_Click(
		object sender,
		EventArgs e)
	{
		using var preferences =
			new Preferences(_settings);

		if (preferences.ShowDialog(this) !=
			DialogResult.OK)
		{
			return;
		}

		var newSettings =
			preferences.CreateSettingsCopy(
				_settings);

		try
		{
			StartupManager.SetEnabled(
				newSettings.StartWithWindows);
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				this,
				"Windows startup could not be changed.\n\n" +
				ex.Message,
				"BatteryCheck",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning);

			newSettings.StartWithWindows =
				StartupManager.IsEnabled();
		}

		_settings = newSettings;

		_refreshTimer.Interval =
			Math.Clamp(
				_settings.RefreshIntervalSeconds,
				10,
				3600) * 1000;

		AppSettingsStore.Save(_settings);
	}

	private void mnuHelpAbout_Click(
		object sender,
		EventArgs e)
	{
		MessageBox.Show(
			this,
			"BatteryCheck\n\n" +
			"A lightweight Windows Bluetooth battery monitor.\n\n" +
			"Battery percentages are shown when a device " +
			"exposes a standard battery service to Windows.",
			"About BatteryCheck",
			MessageBoxButtons.OK,
			MessageBoxIcon.Information);
	}

	private void Form1_FormClosing(
		object? sender,
		FormClosingEventArgs e)
	{
		Rectangle bounds =
			WindowState == FormWindowState.Normal
				? Bounds
				: RestoreBounds;

		_settings.HasSavedWindowBounds = true;

		if (_settings.RememberWindowPosition)
		{
			_settings.WindowX = bounds.X;
			_settings.WindowY = bounds.Y;
		}

		if (_settings.RememberWindowSize)
		{
			_settings.WindowWidth = bounds.Width;
			_settings.WindowHeight = bounds.Height;
		}

		AppSettingsStore.Save(_settings);
	}
}