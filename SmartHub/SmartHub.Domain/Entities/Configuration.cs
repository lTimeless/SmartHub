using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.Entities
{
	public class Configuration : BaseEntity
	{
		/// <summary>
		/// Tells what kind of setting this is
		/// </summary>
		public ConfigurationTypes Type { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDefault { get; private set; }
		public string PluginPath { get; private set; }
		public string DownloadServerUrl { get; private set; }

		public Configuration()
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
