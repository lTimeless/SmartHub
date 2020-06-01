using SmartHub.Application.UseCases.Entity.Settings;
using System.Collections.Generic;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateResponseDto : BaseDto
	{
		public List<SettingDto> Settings { get; set; }
	}
}
