using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.DeviceState
{
	public record DeviceLightStateResponse(bool Ison, string Mode, int Red, int Green, int Blue, int White);
	public class DeviceStatePayload : Payload
	{
		public DeviceLightStateResponse? DeviceLightStateResponse { get; }
		public DeviceStatePayload(DeviceLightStateResponse? deviceLightStateResponse, string? message = null) : base(message)
		{
			DeviceLightStateResponse = deviceLightStateResponse;
		}

		public DeviceStatePayload(UserError error) : base(new []{ error })
		{
		}
	}
}