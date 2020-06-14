using System;

namespace SmartHub.Domain.Enums
{
	[Flags]
	public enum SettingTypeEnum
	{
		Default = 0,
		Plugin = 1,
		Basic = 2,
		Advanced = 4
	}
}
