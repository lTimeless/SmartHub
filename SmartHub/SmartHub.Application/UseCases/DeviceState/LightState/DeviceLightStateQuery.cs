using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateQuery : IRequest<Response<DeviceStateDto>>
	{
		public DeviceLightStateRequestDto LightStateDto { get; }

		public DeviceLightStateQuery(DeviceLightStateRequestDto lightStateDto)
		{
			LightStateDto = lightStateDto;
		}
	}
}
