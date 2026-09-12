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

		btnCreateDesktopShortcut = new Button();
		btnOK = new Button();
		btnCancel = new Button();

		grpGeneral.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)nudRefreshInterval).BeginInit();

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

		// btnCreateDesktopShortcut
		btnCreateDesktopShortcut.Location =
			new Point(12, 211);

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
			new Point(262, 213);

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
			new Point(343, 213);

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
			new Size(430, 255);

		Controls.Add(grpGeneral);
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

		((System.ComponentModel.ISupportInitialize)
			nudRefreshInterval).EndInit();

		ResumeLayout(false);
	}
}