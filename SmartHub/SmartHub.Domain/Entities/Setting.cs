using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.Entities
{
	public class Setting : BaseEntity
	{
		/// <summary>
		/// Tells what kind of setting this is
		/// </summary>
		public SettingTypes Type { get; }
		public bool IsActive { get; private set; }
		public bool IsDefault { get; }
		public string PluginPath { get; } = default!;
		public string DownloadServerUrl { get; } = default!;

		private Setting()
		{
		}

		public Setting(string name, string description, bool isActive, string pluginPath, string downloadServerUrl,
			SettingTypes type) :
			base(name, description)
		{
			IsActive = isActive;
			PluginPath = pluginPath;
			IsDefault = type == SettingTypes.Default;
			DownloadServerUrl = downloadServerUrl;
			Type = type;
		}
	}
}
