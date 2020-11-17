using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common;
using System;
using YamlDotNet.Serialization;

namespace SmartHub.Application.UseCases.HomeFolder.HomeConfigParser
{
	public class ConfigService : IConfigService
	{
		private readonly IFileService _fileService;
		private readonly IOptionsMonitor<HomeConfiguration> _homeConfig;

		public ConfigService(IFileService fileService, IOptionsMonitor<HomeConfiguration> homeConfig)
		{
			_fileService = fileService;
			_homeConfig = homeConfig;
		}

		/// <inheritdoc cref="IConfigService.CreateYamlConfigFile()"/>
		public void CreateYamlConfigFile()
		{
			// Hier die homeConfig zu einem string convertieren und dann an dem yaml serializer geben
			// dann den yaml string zu einer datei schreiben
			var serializer = new SerializerBuilder().Build();
			var homeConfig = new { SmartHub = _homeConfig.CurrentValue};
			homeConfig.SmartHub.TimeZone = TimeZoneInfo.Local.DisplayName;
			var yaml = serializer.Serialize(homeConfig);
			var filePath = _homeConfig.CurrentValue.GetConfigFilePath();
			_fileService.CreateFile(filePath, yaml);
		}

		/// <inheritdoc cref="IConfigService.ReadYamlConfigFile()"/>
		public void ReadYamlConfigFile()
		{
			var filePath = _homeConfig.CurrentValue.GetConfigFilePath();
			var readfileString = _fileService.ReadFileAsString(filePath);
		}
	}
}
