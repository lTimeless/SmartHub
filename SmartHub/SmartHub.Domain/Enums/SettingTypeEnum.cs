using System;

namespace SmartHub.Domain.Enums
{
	[Flags]
	public enum SettingTypeEnum
	{
		Default,
		Plugin,
		Basic,
		Advanced
	}
}
