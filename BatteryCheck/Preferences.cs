using BatteryCheck.Services;
using BatteryCheck.Settings;

namespace BatteryCheck;

public partial class Preferences : Form
{
	public Preferences(AppSettings settings)
	{
		InitializeComponent();

		chkStartWithWindows.Checked =
			settings.StartWithWindows;

		chkRememberWindowSize.Checked =
			settings.RememberWindowSize;

		chkRememberWindowPosition.Checked =
			settings.RememberWindowPosition;

		nudRefreshInterval.Value =
			Math.Clamp(
				settings.RefreshIntervalSeconds,
				(int)nudRefreshInterval.Minimum,
				(int)nudRefreshInterval.Maximum);
	}

	public AppSettings CreateSettingsCopy(
		AppSettings existing) => new()
		{
			StartWithWindows =
				chkStartWithWindows.Checked,

			RememberWindowSize =
				chkRememberWindowSize.Checked,

			RememberWindowPosition =
				chkRememberWindowPosition.Checked,

			RefreshIntervalSeconds =
				(int)nudRefreshInterval.Value,

			HasSavedWindowBounds =
				existing.HasSavedWindowBounds,

			WindowX =
				existing.WindowX,

			WindowY =
				existing.WindowY,

			WindowWidth =
				existing.WindowWidth,

			WindowHeight =
				existing.WindowHeight
		};

	private void btnCreateDesktopShortcut_Click(
		object sender,
		EventArgs e)
	{
		try
		{
			string shortcutPath =
				DesktopShortcutManager
					.CreateDesktopShortcut();

			MessageBox.Show(
				this,
				"Desktop shortcut created here:\n\n" +
				shortcutPath,
				"BatteryCheck",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				this,
				"BatteryCheck could not create the desktop shortcut.\n\n" +
				ex.ToString(),
				"BatteryCheck",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}
	}
}