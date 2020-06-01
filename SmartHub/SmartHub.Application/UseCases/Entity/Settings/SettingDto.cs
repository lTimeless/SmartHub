using SmartHub.Domain.Enums;

namespace SmartHub.Application.UseCases.Entity.Settings
{
	public class SettingDto : BaseDto
	{
		public string HomeId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDefault { get; set; }
		public string PluginPath { get; set; }
		public SettingTypeEnum Type { get; set; }
		public string SettingCreator { get; set; }
	}
}
