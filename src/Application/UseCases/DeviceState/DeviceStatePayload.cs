using SmartHub.Application.Common.Models;
using SmartHub.BasePlugin.Types;

namespace SmartHub.Application.UseCases.DeviceState
{
	public class DeviceStatePayload : Payload
	{
		public LightResponseType? LightResponseType { get; }
		public DeviceStatePayload(LightResponseType? lightResponseType, string? message = null) : base(message)
		{
			LightResponseType = lightResponseType;
		}

		public DeviceStatePayload(UserError error) : base(new []{ error })
		{
		}
	}
}