namespace BatteryCheck.Models
{
	public class DeviceInfo
	{
		public string Name { get; set; } = "";

		public ulong Address { get; set; }

		public int Battery { get; set; } = -1;

		public bool Connected { get; set; }

		public DateTime LastUpdated { get; set; } = DateTime.Now;

		public override string ToString()
		{
			return Name;
		}
	}
}