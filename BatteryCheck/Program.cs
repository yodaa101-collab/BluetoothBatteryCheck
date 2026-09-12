namespace BatteryCheck
{
	internal static class Program
	{
		private static readonly string DisclaimerFolder =
			Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"BatteryCheck");

		private static readonly string DisclaimerFile =
			Path.Combine(DisclaimerFolder, "disclaimer.accepted");

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();

			if (!HasAcceptedDisclaimer())
			{
				using var disclaimer = new DisclaimerForm();

				if (disclaimer.ShowDialog() != DialogResult.OK)
					return;

				SaveDisclaimerAcceptance();
			}

			Application.Run(new Form1());
		}

		private static bool HasAcceptedDisclaimer()
		{
			try
			{
				return File.Exists(DisclaimerFile);
			}
			catch
			{
				return false;
			}
		}

		private static void SaveDisclaimerAcceptance()
		{
			try
			{
				Directory.CreateDirectory(DisclaimerFolder);

				File.WriteAllText(
					DisclaimerFile,
					$"Accepted: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
			}
			catch
			{
				// If Windows prevents saving the acceptance,
				// BatteryCheck can still continue normally.
			}
		}
	}

	internal sealed class DisclaimerForm : Form
	{
		public DisclaimerForm()
		{
			Text = "BatteryCheck - Disclaimer";
			StartPosition = FormStartPosition.CenterScreen;
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			ShowInTaskbar = true;
			ClientSize = new Size(560, 430);
			Font = new Font("Segoe UI", 9F);

			var titleLabel = new Label
			{
				Text = "BatteryCheck Disclaimer",
				Font = new Font("Segoe UI", 16F, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(25, 22)
			};

			var introLabel = new Label
			{
				Text = "Please read the following before using BatteryCheck.",
				Font = new Font("Segoe UI", 9.5F),
				AutoSize = true,
				Location = new Point(27, 62)
			};

			var disclaimerText = new TextBox
			{
				Multiline = true,
				ReadOnly = true,
				ScrollBars = ScrollBars.Vertical,
				TabStop = false,
				Location = new Point(30, 95),
				Size = new Size(500, 245),
				BackColor = SystemColors.Window,
				Text =
					"BatteryCheck is provided \"as is\" without warranties of any kind." +
					Environment.NewLine +
					Environment.NewLine +
					"BatteryCheck interacts with Windows Bluetooth functionality, " +
					"including device discovery, battery information, pairing, " +
					"unpairing and application startup settings." +
					Environment.NewLine +
					Environment.NewLine +
					"Behaviour may vary depending on your Windows version, computer, " +
					"Bluetooth hardware, drivers and connected devices." +
					Environment.NewLine +
					Environment.NewLine +
					"The developer is not responsible for system instability, crashes, " +
					"loss of data, Bluetooth connectivity problems, device configuration " +
					"changes, hardware or software problems, or other damage arising from " +
					"the installation or use of BatteryCheck." +
					Environment.NewLine +
					Environment.NewLine +
					"Use this application at your own discretion. Important information " +
					"and system data should always be backed up appropriately."
			};

			var acceptanceLabel = new Label
			{
				Text = "By selecting I Accept, you acknowledge and accept this disclaimer.",
				AutoSize = true,
				Location = new Point(30, 355)
			};

			var acceptButton = new Button
			{
				Text = "I Accept",
				DialogResult = DialogResult.OK,
				Size = new Size(110, 34),
				Location = new Point(300, 385)
			};

			var exitButton = new Button
			{
				Text = "Exit",
				DialogResult = DialogResult.Cancel,
				Size = new Size(110, 34),
				Location = new Point(420, 385)
			};

			AcceptButton = acceptButton;
			CancelButton = exitButton;

			Controls.Add(titleLabel);
			Controls.Add(introLabel);
			Controls.Add(disclaimerText);
			Controls.Add(acceptanceLabel);
			Controls.Add(acceptButton);
			Controls.Add(exitButton);
		}
	}
}