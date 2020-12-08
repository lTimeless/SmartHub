using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateQuery : IRequest<DeviceStateDto?>
	{
		// TODO hier aufräumen
		public DeviceLightStateRequestDto LightStateDto { get; }

		public DeviceLightStateQuery(DeviceLightStateRequestDto lightStateDto)
		{
			LightStateDto = lightStateDto;
		}
	}
}
