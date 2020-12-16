using System;

namespace SmartHub.Domain.Common.Enums
{
	[Flags]
	public enum ConnectionTypes
	{
		None = 0,
		Http = 1,
		Mqtt = 2
	}
}
