using System;

namespace SmartHub.Domain.Enums
{
	[Flags]
	public enum ConnectionTypes
	{
		None = 0,
		Http = 1,
		Mqtt = 2,
	}
}
