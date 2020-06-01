namespace SmartHub.Application.UseCases.DeviceState
{
	public abstract class DeviceStateDto
	{
		public string DeviceId { get; set; }
		public string Company { get; set; }

		// on = true
		public bool? ToggleLight { get; set; }
	}
}
