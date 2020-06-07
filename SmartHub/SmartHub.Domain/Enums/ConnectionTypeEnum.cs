using System;

namespace SmartHub.Domain.Enums
{
	[Flags]
	public enum ConnectionTypeEnum
	{
		Http,
		Mqtt,
		None
	}
}
