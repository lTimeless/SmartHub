using SmartHub.Domain.Entities.ValueObjects;
using System.IO;

namespace SmartHub.Domain
{
	/// <summary>
	///     This contains all settings for the smarthome.
	/// </summary>
	public class AppConfig
	{
		// TODO make it a record ??! 🤔🤔
		// TODO clean this up

		// TODO save all downloaded pluginNames -> will be implemented with the "Download plugin from cloud UseCase"

		/// <summary>
		///     The app name.
		/// </summary>
		public string? ApplicationName { get; set; }

		public string? ConfigName { get; set; }
		public string? Description { get; set; }
		public Address? Address { get; set; }

		/// <summary>
		///     Defines if the Home is created or not.
		///     true = created
		/// </summary>
		public bool IsActive { get; set; }

		public string? UnitSystem { get; set; }
		public string? TimeZone { get; set; }

		public string? DownloadServerUrl { get; set; }
		public string? BaseFolderName { get; set; }

		public string? ConfigFolderName { get; set; }
		public string? ConfigFolderPath { get; set; }
		public string? ConfigFileName { get; set; }

		public string? PluginFolderName { get; set; }
		public string? PluginFolderPath { get; set; }

		public string? LogFolderName { get; set; }
		public string? LogFolderPath { get; set; }

		public bool FirstStartUp { get; set; } = false;

		/// <summary>
		///     The offset that will be added to the amount of deleted items if the limit is reached.
		/// </summary>
		public int? DeleteXAmountAfterLimit { get; set; }

		/// <summary>
		///     The limit for saving items.
		/// </summary>
		public int? SaveXLimit { get; set; }

		#region Methods

		public string GetConfigFilePath()
		{
			return ConfigFolderPath + Path.DirectorySeparatorChar + ConfigFileName;
		}

		public string GetLogFilePath()
		{
			return LogFolderPath + Path.DirectorySeparatorChar + LogFolderName;
		}

		#endregion
	}
}