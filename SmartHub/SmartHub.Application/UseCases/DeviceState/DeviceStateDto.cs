namespace SmartHub.Application.UseCases.DeviceState
{
	public abstract class DeviceStateDto
	{
		public string? DeviceId { get; set; }
		public bool? ToggleLight { get; set; } // on = true
	}
}
