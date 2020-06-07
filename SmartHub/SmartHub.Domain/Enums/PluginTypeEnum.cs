using System;

namespace SmartHub.Domain.Enums
{
	[Flags]
	public enum PluginTypeEnum
	{
		Base,
		Mock,
		Door,
		Light,
		Ht, // humidity and temperature sensor
		Sensor, //  default if it is not defined
		Rgb, // red green blue
		None
	}
}
