using System;

namespace SmartHub.Domain.Common.Enums
{
	[Flags]
	public enum SettingTypes
	{
		Default = 0,
		Plugin = 1,
		Basic = 2,
		Advanced = 4
	}
}
