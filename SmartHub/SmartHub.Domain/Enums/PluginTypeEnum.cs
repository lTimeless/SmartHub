namespace SmartHub.Domain.Enums
{
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
