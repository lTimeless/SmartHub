using MediatR;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.DeviceState.SettingState
{
	public class DeviceSettingStateQuery : IRequest<ServiceResponse<DeviceSettingStateRequestDto>>
	{
		public DeviceSettingStateRequestDto DeviceSettingsStateDto { get; }

		public DeviceSettingStateQuery(DeviceSettingStateRequestDto deviceSettingsStateDto)
		{
			DeviceSettingsStateDto = deviceSettingsStateDto;
		}
	}
}
