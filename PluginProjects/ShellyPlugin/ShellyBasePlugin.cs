using System;

namespace ShellyPlugin
{
	public class ShellyBasePlugin : IShellyBasePlugin
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Name { get; set; } = $"{nameof(ShellyBasePlugin)}";
		public bool MqttSupport { get; set; } = true;
		public bool HttpSupport { get; set; } = false;

		public string Company { get; set; } = $"{typeof(ShellyBasePlugin).Assembly.FullName.Split("P")[0]}";

		public double AssemblyVersion { get; set; }
		public string Query { get; set; }

		public DateTime ModifiedAt { get; set; }

		public DateTime CreatedAt { get; set; }

		#region HTTP

		// All Shelly
		public string Shelly { get; set; } = "/shelly";

		public string Settings { get; set; } = "/settings";
		public string SettingsAp { get; set; } = "/settings/ap";
		public string SettingsSta { get; set; } = "/settings/sta";

		// static ip bsp. => /settings/sta?ipv4_method=static&ip=192.168.2.101&netmask=255.255.255.0&gateway=192.168.2.1
		public string SettingsLogin { get; set; } = "/settings/login";

		public string SettingsCloud { get; set; } = "/settings/cloud";

		public string Status { get; set; } = "/status";
		public string Reboot { get; set; } = "/reboot";
		public string TurnOnOff { get; set; } = "turn=";

		// Shelly 1
		public string SettingsRelay { get; set; } = "/settings/relay/0";

		public string Relay { get; set; } = "/relay/0";

		// Shelly RGBW2
		public string SettingsColor { get; set; } = "/settings/color/0";

		public string SettingsWhite { get; set; } = "/settings/white/<n>"; //bsp. =  /white/1?brightness=52

		// n 1 -4  damit kann man einzelne farben nur ansteuern
		public string Color { get; set; } = "/color/0"; // bsp. = /color/0?turn=on&red=220&green=0&blue=2&white=0

		// turn=on/off kann man weglassen

		// Settings  => zwischen einzel farben oder alle auf einmal ansprechen
		// settings/?mode=color => alle  settings/?mode=white => einzel

		#endregion HTTP

		public ShellyBasePlugin()
		{
		}

		public void Execute()
		{
			throw new NotImplementedException();
		}

		public string ExecuteHttp()
		{
			throw new NotImplementedException();
		}

		public string ExecuteToggleLightHttp(bool on)
		{
			throw new NotImplementedException();
		}

		public string ExecuteMqtt()
		{
			throw new NotImplementedException();
		}

		public string ExecuteSetting()
		{
			throw new NotImplementedException();
		}
	}
}
