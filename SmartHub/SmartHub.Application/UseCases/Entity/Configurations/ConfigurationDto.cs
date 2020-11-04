using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Configurations
{
	public class ConfigurationDto : BaseDto, IMapFrom<Configuration>
	{
		public ConfigurationTypes Type { get; set; }
		public bool IsActive { get; set; }
		public bool IsDefault { get; set; }

		public string? WatchPathAbsolut { get; set; }
		public string? PluginPath { get; set; }
		public string? DownloadServerUrl { get; set; }
		public string? Filepath { get; set; }
	}
}
