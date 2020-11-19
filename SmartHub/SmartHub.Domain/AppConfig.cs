using SmartHub.Domain.Entities.ValueObjects;
using System.IO;

namespace SmartHub.Domain
{
	/// <summary>
	/// This contains all settings for the smarthome
	/// </summary>
	public class AppConfig
	{
		/// <summary>
		/// The app name
		/// </summary>
		public string? ApplicationName { get; set; }
		public string? ConfigName { get; set; }
		public string? Description { get; set; }
		public Address? Address { get; set; }

		/// <summary>
		/// Defines if the Home is created or not
		/// true = created
		/// </summary>
		public bool IsActive { get; set; }

		public string? UnitSystem { get; set; }
		public string? TimeZone { get; set; }

		public string? DownloadServerUrl { get; set; }
		public string? BaseFolderName { get; set; }

		public string? ConfigFolderName { get; set; }
		public string? ConfigFolderPath { get; set; }
		public string? ConfigFileName { get; set; }
		public string? ConfigFilePath { get; set; }

		public string? PluginFolderName { get; set; }
		public string? PluginFolderPath { get; set; }

		public string? LogFolderName { get; set; }
		public string? LogFolderPath { get; set; }

		public int? DeleteXAmountAfterLimit { get; set; }
		public int? SaveXLimit { get; set; }

		#region Methods

		public string GetConfigFilePath()
		{
			return ConfigFolderPath + Path.DirectorySeparatorChar + ConfigFileName;

		}
		#endregion
	}
}
