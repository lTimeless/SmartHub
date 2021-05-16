using SmartHub.Application.Common.Interfaces;
using System;
using Serilog;
using YamlDotNet.Serialization;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Domain;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SmartHub.Application.UseCases.AppFolder.AppConfigParser
{
	/// <inheritdoc cref="IAppConfigService"/>
	public class AppConfigService : IAppConfigService
	{
		private readonly IFileService _fileService;
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger = Log.ForContext(typeof(AppConfigService));
		private AppConfig _appConfig = new();

		public AppConfigService(IFileService fileService, IConfiguration configuration)
		{
			_fileService = fileService;
			_configuration = configuration;
			configuration.GetSection("SmartHub").Bind(_appConfig);
		}

		/// <inheritdoc cref="IAppConfigService.GetConfig"/>
		public AppConfig GetConfig() => _appConfig;

		/// <inheritdoc cref="IAppConfigService.UpdateConfigAsync"/>
		public async Task UpdateConfigAsync(AppConfig newAppConfig)
		{
			// update file
			await UpdateFile(newAppConfig);
			// update cache
			_appConfig = newAppConfig;
		}

		/// <inheritdoc cref="IAppConfigService.CreateOrGetConfigFile"/>
		public void CreateOrGetConfigFile()
		{
			var yaml = CreateYamlString(_appConfig);
			var filePath = _appConfig.GetConfigFilePath();
			var fileCreated = _fileService.CreateFile(filePath, yaml);
			if (fileCreated)
			{
				_logger.Information("Created yaml-config-file");
				return;
			}
			// Read file if it already exist and override _appConfig
			var config = ReadConfigFile();
			if (config is not null)
			{
				_appConfig = config;
				_logger.Information("Read yaml-config-file into appConfig");
				return;
			}
			_logger.Warning($"YamlConfig is null: {config}");
			return;
		}


		/// <inheritdoc cref="IAppConfigService.SaveConfigAsync"/>
		public async Task SaveConfigAsync()
		{
			// update file
			await UpdateFile(_appConfig);
		}


		/// <summary>
		/// Reads the config-File.
		/// </summary>
		/// <returns>Returns an AppConfig class with the properties from the file or null.</returns>
		private AppConfig ReadConfigFile()
		{
			var filePath = _appConfig.GetConfigFilePath();
			var configAsString = _fileService.ReadFileAsString(filePath);
			var deSerializer = new DeserializerBuilder().Build();
			var yamlConfig = deSerializer.Deserialize<YamlConfigStructure>(configAsString);
			return yamlConfig.ApplicationConfig ?? _appConfig;
		}

		/// <summary>
		/// Updates the config file with the new config properties.
		/// </summary>
		/// <param name="newAppConfig"></param>
		/// <returns>Returns true if it was successful.</returns>
		private async Task<bool> UpdateFile(AppConfig newAppConfig)
		{
			var fileOverride = await OverrideConfigFile(newAppConfig);
			if (fileOverride)
			{
				_logger.Information("Updated yaml-config-file.");
				return true;
			}
			return false;
		}

		/// <summary>
		/// Overrides the existing config-File.
		/// </summary>
		/// <returns>Returns true if it was successful.</returns>
		private Task<bool> OverrideConfigFile(AppConfig newAppConfig)
		{
			var yaml = CreateYamlString(newAppConfig);
			var filePath = _appConfig.GetConfigFilePath();
			return Task.FromResult(_fileService.OverrideFile(filePath, yaml));
		}

		/// <summary>
		/// Creates a yaml string from the AppConfig class.
		/// </summary>
		/// <returns>Returns a string looking like yaml.</returns>
		private string CreateYamlString(AppConfig newAppConfig)
		{
			var serializer = new SerializerBuilder().Build();
			_appConfig.TimeZone = TimeZoneInfo.Local.DisplayName;
			var homeConfig = new YamlConfigStructure { ApplicationConfig = newAppConfig };
			var yaml = serializer.Serialize(homeConfig);
			return yaml;
		}
	}
}
