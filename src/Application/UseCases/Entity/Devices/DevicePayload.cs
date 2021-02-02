using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Devices
{
	public class DevicePayload : Payload
	{
		public Device? Device { get; }

		public DevicePayload(Device? device, string? message = null) : base(message)
		{
			Device = device;
		}

		public DevicePayload(UserError error) : base(new []{ error })
		{
		}
	}
}