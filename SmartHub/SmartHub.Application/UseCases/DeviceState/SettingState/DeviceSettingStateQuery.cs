using MediatR;

namespace SmartHub.Application.UseCases.DeviceState.SettingState
{
	public class DeviceSettingStateQuery : IRequest<DeviceSettingStateRequestDto?>
	{
		// TODO hier aufräumen
		public DeviceSettingStateRequestDto DeviceSettingsStateDto { get; }

		public DeviceSettingStateQuery(DeviceSettingStateRequestDto deviceSettingsStateDto)
		{
			DeviceSettingsStateDto = deviceSettingsStateDto;
		}
	}
}
