namespace SmartHub.Application.Common.Models
{
	public record DeviceStateResponse(bool Ison, string Mode, int Red, int Green, int Blue, int White);
}