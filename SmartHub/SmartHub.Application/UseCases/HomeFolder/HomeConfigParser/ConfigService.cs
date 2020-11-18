using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using SmartHub.Domain.Entities;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace SmartHub.Application.UseCases.HomeFolder.HomeConfigParser
{
	public class ConfigService : IConfigService
	{
		private readonly IFileService _fileService;
		private IOptions<ApplicationConfig> _homeConfig;
		private readonly ILogger _logger = Log.ForContext(typeof(ConfigService));

		public ConfigService(IFileService fileService, IOptions<ApplicationConfig> homeConfig)
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
				return;
			}
			var configAsString = ReadConfigFileAsString();
			var deSerializer = new DeserializerBuilder().Build();
			var yamlConfig = deSerializer.Deserialize<YamlConfigStructure>(configAsString);
			if (yamlConfig.ApplicationConfig is not null)
			{
				UpdateAppConfigFromFile(yamlConfig.ApplicationConfig);
				return;
			}
			_logger.Warning($"YamlConfig is null: {yamlConfig}");
		}

		/// <inheritdoc cref="IConfigService.ReadConfigFileAsString()"/>
		public string ReadConfigFileAsString()
		{
			var filePath = _homeConfig.Value.GetConfigFilePath();
			return _fileService.ReadFileAsString(filePath);
		}

		public void UpdateConfigFileFromString()
		{
			throw new NotImplementedException();
		}

		public void UpdateConfigFileFromSystem()
		{
			var yamlString = ReadConfigFileAsString();
			// hier dann den string deserialisieren und dann die _homeConfig überschreiben

		}

		public void ValidateConfigFile()
		{
			throw new NotImplementedException();
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

		private void UpdateAppConfigFromFile(ApplicationConfig newConfig)
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
