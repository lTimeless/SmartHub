using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.DeviceState
{
	public class DeviceStatePayload : Payload
	{
		public DeviceStateResponse? DeviceLightStateResponse { get; }
		public DeviceStatePayload(DeviceStateResponse? deviceLightStateResponse, string? message = null) : base(message)
		{
			DeviceLightStateResponse = deviceLightStateResponse;
		}

		public DeviceStatePayload(UserError error) : base(new []{ error })
		{
		}
	}
}