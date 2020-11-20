using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces;
using System;
using Serilog;
using YamlDotNet.Serialization;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Domain;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.AppFolder.AppConfigParser
{
	/// <inheritdoc cref="IAppConfigService"/>
	public class AppConfigService : IAppConfigService
	{
		private readonly IFileService _fileService;
		private readonly IOptions<AppConfig> _homeConfig;
		private readonly ILogger _logger = Log.ForContext(typeof(AppConfigService));

		public AppConfigService(IFileService fileService, IOptions<AppConfig> homeConfig)
		{
			_fileService = fileService;
			_homeConfig = homeConfig;
		}

		/// <inheritdoc cref="IAppConfigService.CreateOrReadConfigFile()"/>
		public async Task CreateOrReadConfigFile()
		{
			var fileCreated = await CreateConfigFile();
			if (fileCreated)
			{
				_logger.Information("Created yaml-config-file.");
				return;
			}
			var configAsString = await ReadConfigFileAsString();
			var deSerializer = new DeserializerBuilder().Build();
			var yamlConfig = deSerializer.Deserialize<YamlConfigStructure>(configAsString);
			if (yamlConfig.ApplicationConfig is not null)
			{
				UpdateAppConfig(yamlConfig.ApplicationConfig);
				_logger.Information("Read yaml-config-file and updated appConfig.");
				return;
			}
			_logger.Warning($"YamlConfig is null: {yamlConfig} .");
			return;
		}

		/// <inheritdoc cref="IAppConfigService.ReadConfigFileAsString()"/>
		public Task<string> ReadConfigFileAsString()
		{
			var filePath = _homeConfig.Value.GetConfigFilePath();
			return Task.FromResult(_fileService.ReadFileAsString(filePath));
		}

		public Task ValidateConfigFile()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc cref="IAppConfigService.UpdateFileFromClass"/>
		public async Task<bool> UpdateFileFromClass()
		{
			var fileOverride = await OverrideConfigFile();
			if (fileOverride)
			{
				_logger.Information("Updated yaml-config-file.");
				return true;
			}
			return false;
		}

		private Task<bool> CreateConfigFile()
		{
			var serializer = new SerializerBuilder().Build();
			_homeConfig.Value.TimeZone = TimeZoneInfo.Local.DisplayName;
			var homeConfig = new YamlConfigStructure { ApplicationConfig = _homeConfig.Value };
			var yaml = serializer.Serialize(homeConfig);
			var filePath = _homeConfig.Value.GetConfigFilePath();
			return Task.FromResult(_fileService.CreateFile(filePath, yaml));
		}

		private Task<bool> OverrideConfigFile()
		{
			var serializer = new SerializerBuilder().Build();
			_homeConfig.Value.TimeZone = TimeZoneInfo.Local.DisplayName;
			var homeConfig = new YamlConfigStructure { ApplicationConfig = _homeConfig.Value };
			var yaml = serializer.Serialize(homeConfig);
			var filePath = _homeConfig.Value.GetConfigFilePath();
			return Task.FromResult(_fileService.OverrideFile(filePath, yaml));
		}

		private void UpdateAppConfig(AppConfig newConfig)
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
