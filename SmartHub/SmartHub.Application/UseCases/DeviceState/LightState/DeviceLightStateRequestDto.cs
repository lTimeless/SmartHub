using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateRequestDto : DeviceStateDto
	{
		// Color 0 - 255
		public int? Red { get; set; }
		public int? Green { get; set; }
		public int? Blue { get; set; }
		public int? Alpha { get; set; }
		public string? Effect { get; set; }
	}
}
