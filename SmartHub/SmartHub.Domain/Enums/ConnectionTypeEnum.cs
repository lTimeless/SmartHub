using System;

namespace SmartHub.Domain.Enums
{
	[Flags]
	public enum ConnectionTypeEnum
	{
		None = 0,
		Http = 1,
		Mqtt = 2,
	}
}
