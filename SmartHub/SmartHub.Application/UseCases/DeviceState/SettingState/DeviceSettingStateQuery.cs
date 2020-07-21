using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.DeviceState.SettingState
{
	public class DeviceSettingStateQuery : IRequest<Response<DeviceSettingStateRequestDto>>
	{
		public DeviceSettingStateRequestDto DeviceSettingsStateDto { get; }

		public DeviceSettingStateQuery(DeviceSettingStateRequestDto deviceSettingsStateDto)
		{
			DeviceSettingsStateDto = deviceSettingsStateDto;
		}
	}
}
