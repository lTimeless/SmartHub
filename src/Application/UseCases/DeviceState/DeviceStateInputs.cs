namespace SmartHub.Application.UseCases.DeviceState
{
	public record DeviceLightStateInput(string DeviceId, bool SetLight);

	// public record DeviceLightStateRgbInput : DeviceLightStateInput()
	// {
	// 	// Color 0 - 255
	// 	public int? Red { get; set; }
	// 	public int? Green { get; set; }
	// 	public int? Blue { get; set; }
	// 	public int? Alpha { get; set; }
	// 	public string? Effect { get; set; }
	// }
}