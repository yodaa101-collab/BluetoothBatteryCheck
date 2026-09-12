#nullable disable

namespace BatteryCheck;

partial class Form1
{
	private System.ComponentModel.IContainer components = null;

	private MenuStrip menuStrip;
	private ToolStripMenuItem mnuFile;
	private ToolStripMenuItem mnuFileRefresh;
	private ToolStripMenuItem mnuFileExit;
	private ToolStripMenuItem mnuSettings;
	private ToolStripMenuItem mnuSettingsPreferences;
	private ToolStripMenuItem mnuHelp;
	private ToolStripMenuItem mnuHelpAbout;

	private Panel headerPanel;
	private Label lblTitle;
	private Label lblDeviceCount;

	private Button btnAddDevice;
	private Button btnRemoveDevice;
	private Button btnRefresh;

	private DataGridView gridDevices;
	private DataGridViewTextBoxColumn colDevice;
	private DataGridViewTextBoxColumn colAddress;
	private DataGridViewTextBoxColumn colBattery;
	private DataGridViewTextBoxColumn colStatus;

	private Panel footerPanel;
	private Label lblLastUpdated;
	private Label lblAttribution;
	private Button btnExit;

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			components?.Dispose();
			_refreshTimer?.Dispose();
		}

		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		components = new System.ComponentModel.Container();

		menuStrip = new MenuStrip();
		mnuFile = new ToolStripMenuItem();
		mnuFileRefresh = new ToolStripMenuItem();
		mnuFileExit = new ToolStripMenuItem();

		mnuSettings = new ToolStripMenuItem();
		mnuSettingsPreferences = new ToolStripMenuItem();

		mnuHelp = new ToolStripMenuItem();
		mnuHelpAbout = new ToolStripMenuItem();

		headerPanel = new Panel();
		lblTitle = new Label();
		lblDeviceCount = new Label();

		btnAddDevice = new Button();
		btnRemoveDevice = new Button();
		btnRefresh = new Button();

		gridDevices = new DataGridView();
		colDevice = new DataGridViewTextBoxColumn();
		colAddress = new DataGridViewTextBoxColumn();
		colBattery = new DataGridViewTextBoxColumn();
		colStatus = new DataGridViewTextBoxColumn();

		footerPanel = new Panel();
		lblLastUpdated = new Label();
		lblAttribution = new Label();
		btnExit = new Button();

		menuStrip.SuspendLayout();
		headerPanel.SuspendLayout();
		footerPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)gridDevices).BeginInit();
		SuspendLayout();

		// menuStrip
		menuStrip.Items.AddRange(
			new ToolStripItem[]
			{
				mnuFile,
				mnuSettings,
				mnuHelp
			});

		menuStrip.Location = new Point(0, 0);
		menuStrip.Name = "menuStrip";
		menuStrip.Size = new Size(640, 24);

		// File
		mnuFile.DropDownItems.AddRange(
			new ToolStripItem[]
			{
				mnuFileRefresh,
				new ToolStripSeparator(),
				mnuFileExit
			});

		mnuFile.Text = "File";

		mnuFileRefresh.Text = "Refresh";
		mnuFileRefresh.ShortcutKeys = Keys.F5;
		mnuFileRefresh.Click += mnuFileRefresh_Click;

		mnuFileExit.Text = "Exit";
		mnuFileExit.Click += mnuFileExit_Click;

		// Settings
		mnuSettings.DropDownItems.AddRange(
			new ToolStripItem[]
			{
				mnuSettingsPreferences
			});

		mnuSettings.Text = "Settings";

		mnuSettingsPreferences.Text = "Preferences…";
		mnuSettingsPreferences.Click += mnuSettingsPreferences_Click;

		// Help
		mnuHelp.DropDownItems.AddRange(
			new ToolStripItem[]
			{
				mnuHelpAbout
			});

		mnuHelp.Text = "Help";

		mnuHelpAbout.Text = "About BatteryCheck";
		mnuHelpAbout.Click += mnuHelpAbout_Click;

		// headerPanel
		headerPanel.Controls.Add(lblTitle);
		headerPanel.Controls.Add(lblDeviceCount);
		headerPanel.Controls.Add(btnAddDevice);
		headerPanel.Controls.Add(btnRemoveDevice);
		headerPanel.Controls.Add(btnRefresh);

		headerPanel.Dock = DockStyle.Top;
		headerPanel.Location = new Point(0, 24);
		headerPanel.Name = "headerPanel";
		headerPanel.Padding = new Padding(12, 10, 12, 8);
		headerPanel.Size = new Size(640, 72);

		// lblTitle
		lblTitle.AutoSize = true;
		lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
		lblTitle.Location = new Point(12, 9);
		lblTitle.Text = "BatteryCheck";

		// lblDeviceCount
		lblDeviceCount.AutoSize = true;
		lblDeviceCount.ForeColor = SystemColors.GrayText;
		lblDeviceCount.Location = new Point(14, 39);
		lblDeviceCount.Text = "Scanning Bluetooth devices…";

		// btnAddDevice
		btnAddDevice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		btnAddDevice.AutoSize = true;
		btnAddDevice.Location = new Point(306, 21);
		btnAddDevice.Name = "btnAddDevice";
		btnAddDevice.Size = new Size(104, 30);
		btnAddDevice.Text = "+ Add Device";
		btnAddDevice.UseVisualStyleBackColor = true;
		btnAddDevice.Click += btnAddDevice_Click;

		// btnRemoveDevice
		btnRemoveDevice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		btnRemoveDevice.Location = new Point(416, 21);
		btnRemoveDevice.Name = "btnRemoveDevice";
		btnRemoveDevice.Size = new Size(104, 30);
		btnRemoveDevice.Text = "Remove Device";
		btnRemoveDevice.UseVisualStyleBackColor = true;
		btnRemoveDevice.Enabled = false;
		btnRemoveDevice.Click += btnRemoveDevice_Click;

		// btnRefresh
		btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		btnRefresh.Location = new Point(526, 21);
		btnRefresh.Name = "btnRefresh";
		btnRefresh.Size = new Size(102, 30);
		btnRefresh.Text = "Refresh";
		btnRefresh.UseVisualStyleBackColor = true;
		btnRefresh.Click += btnRefresh_Click;

		// gridDevices
		gridDevices.AllowUserToAddRows = false;
		gridDevices.AllowUserToDeleteRows = false;
		gridDevices.AllowUserToResizeRows = false;
		gridDevices.BackgroundColor = SystemColors.Window;
		gridDevices.BorderStyle = BorderStyle.None;
		gridDevices.ColumnHeadersHeightSizeMode =
			DataGridViewColumnHeadersHeightSizeMode.AutoSize;

		gridDevices.Columns.AddRange(
			new DataGridViewColumn[]
			{
				colDevice,
				colAddress,
				colBattery,
				colStatus
			});

		gridDevices.Dock = DockStyle.Fill;
		gridDevices.Location = new Point(0, 96);
		gridDevices.MultiSelect = false;
		gridDevices.Name = "gridDevices";
		gridDevices.ReadOnly = true;
		gridDevices.SelectionMode =
			DataGridViewSelectionMode.FullRowSelect;

		gridDevices.Size = new Size(640, 218);

		gridDevices.SelectionChanged += gridDevices_SelectionChanged;

		// columns
		colDevice.HeaderText = "Device";
		colDevice.MinimumWidth = 160;
		colDevice.FillWeight = 55F;

		colAddress.HeaderText = "Address";
		colAddress.Visible = false;

		colBattery.HeaderText = "Battery";
		colBattery.MinimumWidth = 100;
		colBattery.FillWeight = 23F;

		colStatus.HeaderText = "Status";
		colStatus.MinimumWidth = 90;
		colStatus.FillWeight = 22F;

		// footerPanel
		footerPanel.Controls.Add(lblLastUpdated);
		footerPanel.Controls.Add(lblAttribution);
		footerPanel.Controls.Add(btnExit);

		footerPanel.Dock = DockStyle.Bottom;
		footerPanel.Location = new Point(0, 314);
		footerPanel.Name = "footerPanel";
		footerPanel.Size = new Size(640, 66);

		// lblLastUpdated
		lblLastUpdated.Anchor =
			AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

		lblLastUpdated.ForeColor = SystemColors.GrayText;
		lblLastUpdated.Location = new Point(12, 5);
		lblLastUpdated.Name = "lblLastUpdated";
		lblLastUpdated.Size = new Size(500, 20);
		lblLastUpdated.Text = "Not updated yet";

		// lblAttribution
		lblAttribution.Anchor =
			AnchorStyles.Bottom | AnchorStyles.Left;

		lblAttribution.AutoSize = true;
		lblAttribution.ForeColor = SystemColors.GrayText;
		lblAttribution.Font = new Font(
			"Segoe UI",
			8F,
			FontStyle.Regular);

		lblAttribution.Location = new Point(12, 41);
		lblAttribution.Name = "lblAttribution";
		lblAttribution.Text =
			"Created by VA with OpenAI assistance";

		// btnExit
		btnExit.Anchor =
			AnchorStyles.Bottom | AnchorStyles.Right;

		btnExit.Location = new Point(530, 30);
		btnExit.Name = "btnExit";
		btnExit.Size = new Size(98, 30);
		btnExit.Text = "Exit";
		btnExit.UseVisualStyleBackColor = true;

		// Reuse the existing File > Exit handler
		btnExit.Click += mnuFileExit_Click;

		// Form1
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;

		ClientSize = new Size(640, 380);

		Controls.Add(gridDevices);
		Controls.Add(footerPanel);
		Controls.Add(headerPanel);
		Controls.Add(menuStrip);

		Font = new Font("Segoe UI", 9F);
		MainMenuStrip = menuStrip;

		MinimumSize = new Size(560, 325);

		Name = "Form1";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "BatteryCheck";

		Load += Form1_Load;

		menuStrip.ResumeLayout(false);
		menuStrip.PerformLayout();

		headerPanel.ResumeLayout(false);
		headerPanel.PerformLayout();

		footerPanel.ResumeLayout(false);
		footerPanel.PerformLayout();

		((System.ComponentModel.ISupportInitialize)gridDevices).EndInit();

		ResumeLayout(false);
		PerformLayout();
	}
}