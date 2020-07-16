using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Enums;

namespace SmartHub.Application.UseCases.Entity.Settings
{
	public class SettingDto : BaseDto, IMapFrom<Setting>
	{
		public SettingTypes Type { get; set; }
		public bool IsActive { get; set; }
		public bool IsDefault { get; set; }

		public string WatchPathAbsolut { get; set; }
		public string PluginPath { get; set; }
		public string DownloadServerUrl { get; set; }
		public string? Filepath { get; set; }

		public string CreatorName { get; set; }
	}
}
