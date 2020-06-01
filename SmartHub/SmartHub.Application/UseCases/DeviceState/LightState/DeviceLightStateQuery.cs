using MediatR;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateQuery : IRequest<ServiceResponse<DeviceStateDto>>
	{
		public DeviceLightStateRequestDto LightStateDto { get; }

		public DeviceLightStateQuery(DeviceLightStateRequestDto lightStateDto)
		{
			LightStateDto = lightStateDto;
		}
	}
}
