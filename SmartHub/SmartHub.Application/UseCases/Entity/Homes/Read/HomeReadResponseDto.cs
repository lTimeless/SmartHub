using SmartHub.Application.UseCases.Entity.Settings;
using System.Collections.Generic;

namespace SmartHub.Application.UseCases.Entity.Homes.Read
{
	public class HomeReadResponseDto : BaseDto
	{
		public List<SettingDto> Settings { get; set; }
	}
}
