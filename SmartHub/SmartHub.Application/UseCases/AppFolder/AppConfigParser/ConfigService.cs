using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces;
using System;
using Serilog;
using YamlDotNet.Serialization;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.AppFolder.AppConfigParser
{
	public class ConfigService : IConfigService
	{
		private readonly IFileService _fileService;
		private readonly IOptions<AppConfig> _homeConfig;
		private readonly ILogger _logger = Log.ForContext(typeof(ConfigService));

		public ConfigService(IFileService fileService, IOptions<AppConfig> homeConfig)
		{
			_fileService = fileService;
			_homeConfig = homeConfig;
		}

		/// <inheritdoc cref="IConfigService.CreateOrReadConfigFile()"/>
		public void CreateOrReadConfigFile()
		{
			var fileCreated = CreateConfigFile();
			if (fileCreated)
			{
				_logger.Information("Created yaml-config-file.");
				return;
			}
			var configAsString = ReadConfigFileAsString();
			var deSerializer = new DeserializerBuilder().Build();
			var yamlConfig = deSerializer.Deserialize<YamlConfigStructure>(configAsString);
			if (yamlConfig.ApplicationConfig is not null)
			{
				UpdateAppConfig(yamlConfig.ApplicationConfig);
				_logger.Information("Read yaml-config-file and updated appConfig.");
				return;
			}
			_logger.Warning($"YamlConfig is null: {yamlConfig} .");
		}

		/// <inheritdoc cref="IConfigService.ReadConfigFileAsString()"/>
		public string ReadConfigFileAsString()
		{
			var filePath = _homeConfig.Value.GetConfigFilePath();
			return _fileService.ReadFileAsString(filePath);
		}

		public void ValidateConfigFile()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc cref="IConfigService.UpdateFileFromClass"/>
		public bool UpdateFileFromClass()
		{
			var fileCreated = CreateConfigFile();
			if (fileCreated)
			{
				_logger.Information("Updated yaml-config-file.");
				return true;
			}
			return false;
		}

		private bool CreateConfigFile()
		{
			var serializer = new SerializerBuilder().Build();
			_homeConfig.Value.TimeZone = TimeZoneInfo.Local.DisplayName;
			var homeConfig = new YamlConfigStructure { ApplicationConfig = _homeConfig.Value };
			var yaml = serializer.Serialize(homeConfig);
			var filePath = _homeConfig.Value.GetConfigFilePath();
			return _fileService.CreateFile(filePath, yaml);
		}

		private void UpdateAppConfig(Domain.AppConfig newConfig)
		{
			_homeConfig.Value.Address = newConfig.Address;
			_homeConfig.Value.Description = newConfig.Description;
			_homeConfig.Value.ApplicationName = newConfig.ApplicationName;
			_homeConfig.Value.ConfigName = newConfig.ConfigName;
			_homeConfig.Value.IsActive = newConfig.IsActive;
			_homeConfig.Value.TimeZone = newConfig.TimeZone;
			_homeConfig.Value.UnitSystem = newConfig.UnitSystem;
			_homeConfig.Value.BaseFolderName = newConfig.BaseFolderName;
			_homeConfig.Value.ConfigFileName = newConfig.ConfigFileName;
			_homeConfig.Value.ConfigFilePath = newConfig.ConfigFilePath;
			_homeConfig.Value.ConfigFolderName = newConfig.ConfigFolderName;
			_homeConfig.Value.ConfigFolderPath = newConfig.ConfigFolderPath;
			_homeConfig.Value.DownloadServerUrl = newConfig.DownloadServerUrl;
			_homeConfig.Value.LogFolderName = newConfig.LogFolderName;
			_homeConfig.Value.LogFolderPath = newConfig.LogFolderPath;
			_homeConfig.Value.PluginFolderName = newConfig.PluginFolderName;
			_homeConfig.Value.PluginFolderPath = newConfig.PluginFolderPath;
			_homeConfig.Value.SaveXLimit = newConfig.SaveXLimit;
			_homeConfig.Value.DeleteXAmountAfterLimit = newConfig.DeleteXAmountAfterLimit;
		}
	}
}
