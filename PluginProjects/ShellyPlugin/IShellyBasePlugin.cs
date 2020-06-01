using PluginBase;
using System;

namespace ShellyPlugin
{
	public interface IShellyBasePlugin : IPlugin
	{
		public string Id { get; set; }
		public bool MqttSupport { get; set; }
		public bool HttpSupport { get; set; }

		public string Company { get; set; }

		public double AssemblyVersion { get; set; }
		DateTime ModifiedAt { get; set; }

		DateTime CreatedAt { get; set; }

		#region HTTP

		// All Shelly
		public string Shelly { get; set; }

		public string Settings { get; set; }
		public string SettingsAp { get; set; }
		public string SettingsSta { get; set; }

		// static ip bsp. => /settings/sta?ipv4_method=static&ip=192.168.2.101&netmask=255.255.255.0&gateway=192.168.2.1
		public string SettingsLogin { get; set; }

		public string SettingsCloud { get; set; }

		public string Status { get; set; }
		public string Reboot { get; set; }
		public string TurnOnOff { get; set; }

		// Shelly 1
		public string SettingsRelay { get; set; }

		public string Relay { get; set; }

		// Shelly RGBW2
		public string SettingsColor { get; set; }

		public string SettingsWhite { get; set; } //bsp. =  /white/1?brightness=52

		// n 1 -4  damit kann man einzelne farben nur ansteuern
		public string Color { get; set; } // bsp. = /color/0?turn=on&red=220&green=0&blue=2&white=0

		// turn=on/off kann man weglassen

		// Settings  => zwischen einzel farben oder alle auf einmal ansprechen
		// settings/?mode=color => alle  settings/?mode=white => einzel

		#endregion HTTP
	}
}
