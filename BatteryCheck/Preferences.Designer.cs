#nullable disable

namespace BatteryCheck;

partial class Preferences
{
	private System.ComponentModel.IContainer components = null;

	private GroupBox grpGeneral;
	private CheckBox chkStartWithWindows;
	private CheckBox chkRememberWindowSize;
	private CheckBox chkRememberWindowPosition;

	private Label lblRefreshInterval;
	private NumericUpDown nudRefreshInterval;

	private GroupBox grpNotifications;
	private CheckBox chkLowBatteryNotifications;
	private Label lblLowBatteryThreshold;
	private NumericUpDown nudLowBatteryThreshold;
	private Label lblPercent;

	private Button btnCreateDesktopShortcut;
	private Button btnOK;
	private Button btnCancel;

	protected override void Dispose(bool disposing)
	{
		if (disposing)
			components?.Dispose();

		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		components = new System.ComponentModel.Container();

		grpGeneral = new GroupBox();
		chkStartWithWindows = new CheckBox();
		chkRememberWindowSize = new CheckBox();
		chkRememberWindowPosition = new CheckBox();

		lblRefreshInterval = new Label();
		nudRefreshInterval = new NumericUpDown();

		grpNotifications = new GroupBox();
		chkLowBatteryNotifications = new CheckBox();
		lblLowBatteryThreshold = new Label();
		nudLowBatteryThreshold = new NumericUpDown();
		lblPercent = new Label();

		btnCreateDesktopShortcut = new Button();
		btnOK = new Button();
		btnCancel = new Button();

		grpGeneral.SuspendLayout();
		grpNotifications.SuspendLayout();

		((System.ComponentModel.ISupportInitialize)
			nudRefreshInterval).BeginInit();

		((System.ComponentModel.ISupportInitialize)
			nudLowBatteryThreshold).BeginInit();

		SuspendLayout();

		// grpGeneral
		grpGeneral.Anchor =
			AnchorStyles.Top |
			AnchorStyles.Left |
			AnchorStyles.Right;

		grpGeneral.Controls.Add(chkStartWithWindows);
		grpGeneral.Controls.Add(chkRememberWindowSize);
		grpGeneral.Controls.Add(chkRememberWindowPosition);
		grpGeneral.Controls.Add(lblRefreshInterval);
		grpGeneral.Controls.Add(nudRefreshInterval);

		grpGeneral.Location = new Point(12, 12);
		grpGeneral.Name = "grpGeneral";
		grpGeneral.Size = new Size(406, 181);
		grpGeneral.TabStop = false;
		grpGeneral.Text = "General";

		// chkStartWithWindows
		chkStartWithWindows.AutoSize = true;
		chkStartWithWindows.Location =
			new Point(18, 29);

		chkStartWithWindows.Text =
			"Start BatteryCheck with Windows";

		// chkRememberWindowSize
		chkRememberWindowSize.AutoSize = true;
		chkRememberWindowSize.Location =
			new Point(18, 59);

		chkRememberWindowSize.Text =
			"Remember window size";

		// chkRememberWindowPosition
		chkRememberWindowPosition.AutoSize = true;
		chkRememberWindowPosition.Location =
			new Point(18, 89);

		chkRememberWindowPosition.Text =
			"Remember window position";

		// lblRefreshInterval
		lblRefreshInterval.AutoSize = true;
		lblRefreshInterval.Location =
			new Point(18, 132);

		lblRefreshInterval.Text =
			"Refresh interval:";

		// nudRefreshInterval
		nudRefreshInterval.Location =
			new Point(127, 128);

		nudRefreshInterval.Minimum = 10;
		nudRefreshInterval.Maximum = 3600;
		nudRefreshInterval.Value = 60;
		nudRefreshInterval.Width = 72;

		var secondsLabel = new Label
		{
			AutoSize = true,
			Location = new Point(205, 132),
			Text = "seconds"
		};

		grpGeneral.Controls.Add(secondsLabel);

		// grpNotifications
		grpNotifications.Anchor =
			AnchorStyles.Top |
			AnchorStyles.Left |
			AnchorStyles.Right;

		grpNotifications.Controls.Add(
			chkLowBatteryNotifications);

		grpNotifications.Controls.Add(
			lblLowBatteryThreshold);

		grpNotifications.Controls.Add(
			nudLowBatteryThreshold);

		grpNotifications.Controls.Add(
			lblPercent);

		grpNotifications.Location =
			new Point(12, 205);

		grpNotifications.Name =
			"grpNotifications";

		grpNotifications.Size =
			new Size(406, 105);

		grpNotifications.TabStop = false;

		grpNotifications.Text =
			"Notifications";

		// chkLowBatteryNotifications
		chkLowBatteryNotifications.AutoSize = true;

		chkLowBatteryNotifications.Location =
			new Point(18, 28);

		chkLowBatteryNotifications.Text =
			"Notify me when a device battery is low";

		// lblLowBatteryThreshold
		lblLowBatteryThreshold.AutoSize = true;

		lblLowBatteryThreshold.Location =
			new Point(18, 65);

		lblLowBatteryThreshold.Text =
			"Low battery threshold:";

		// nudLowBatteryThreshold
		nudLowBatteryThreshold.Location =
			new Point(157, 61);

		nudLowBatteryThreshold.Minimum = 5;
		nudLowBatteryThreshold.Maximum = 50;
		nudLowBatteryThreshold.Value = 20;
		nudLowBatteryThreshold.Width = 60;

		// lblPercent
		lblPercent.AutoSize = true;

		lblPercent.Location =
			new Point(223, 65);

		lblPercent.Text = "%";

		// btnCreateDesktopShortcut
		btnCreateDesktopShortcut.Location =
			new Point(12, 328);

		btnCreateDesktopShortcut.Name =
			"btnCreateDesktopShortcut";

		btnCreateDesktopShortcut.Size =
			new Size(190, 30);

		btnCreateDesktopShortcut.Text =
			"Create Desktop Shortcut";

		btnCreateDesktopShortcut.UseVisualStyleBackColor =
			true;

		btnCreateDesktopShortcut.Click +=
			btnCreateDesktopShortcut_Click;

		// btnOK
		btnOK.Anchor =
			AnchorStyles.Bottom |
			AnchorStyles.Right;

		btnOK.DialogResult =
			DialogResult.OK;

		btnOK.Location =
			new Point(262, 330);

		btnOK.Size =
			new Size(75, 27);

		btnOK.Text = "OK";

		btnOK.UseVisualStyleBackColor =
			true;

		// btnCancel
		btnCancel.Anchor =
			AnchorStyles.Bottom |
			AnchorStyles.Right;

		btnCancel.DialogResult =
			DialogResult.Cancel;

		btnCancel.Location =
			new Point(343, 330);

		btnCancel.Size =
			new Size(75, 27);

		btnCancel.Text =
			"Cancel";

		btnCancel.UseVisualStyleBackColor =
			true;

		// form
		AcceptButton = btnOK;
		CancelButton = btnCancel;

		AutoScaleDimensions =
			new SizeF(7F, 15F);

		AutoScaleMode =
			AutoScaleMode.Font;

		ClientSize =
			new Size(430, 372);

		Controls.Add(grpGeneral);
		Controls.Add(grpNotifications);
		Controls.Add(btnCreateDesktopShortcut);
		Controls.Add(btnOK);
		Controls.Add(btnCancel);

		Font =
			new Font("Segoe UI", 9F);

		FormBorderStyle =
			FormBorderStyle.FixedDialog;

		MaximizeBox = false;
		MinimizeBox = false;

		Name = "Preferences";
		ShowIcon = false;
		ShowInTaskbar = false;

		StartPosition =
			FormStartPosition.CenterParent;

		Text = "Preferences";

		grpGeneral.ResumeLayout(false);
		grpGeneral.PerformLayout();

		grpNotifications.ResumeLayout(false);
		grpNotifications.PerformLayout();

		((System.ComponentModel.ISupportInitialize)
			nudRefreshInterval).EndInit();

		((System.ComponentModel.ISupportInitialize)
			nudLowBatteryThreshold).EndInit();

		ResumeLayout(false);
	}
}