using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.Entities
{
	public class Configuration : BaseEntity
	{
		/// <summary>
		/// Tells what kind of setting this is
		/// </summary>
		public ConfigurationTypes Type { get; }
		public bool IsActive { get; }
		public bool IsDefault { get; }
		public string PluginPath { get; } = null!;
		public string DownloadServerUrl { get; } = null!;

		// Needed for ef core
		protected Configuration()
		{
		}

		public Configuration(string name, string description, bool isActive, string pluginPath, string downloadServerUrl,
			ConfigurationTypes type) :
			base(name, description)
		{
			IsActive = isActive;
			PluginPath = pluginPath;
			IsDefault = type == ConfigurationTypes.Default;
			DownloadServerUrl = downloadServerUrl;
			Type = type;
		}
	}
}
