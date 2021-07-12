using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;

namespace SmartHub.Application.UseCases.AppFolder
{
	/// <inheritdoc cref="IAppFolderService"/>
	public class AppFolderService : IAppFolderService
	{
		private readonly IDirectoryService _directoryService;
		private readonly IAppConfigService _appConfigService;
		private readonly IHostEnvironment _hostingEnvironment;

		// The overlaying service for creating the SmartHub config folder
		// with functions to create, delete and update it
		public AppFolderService(IDirectoryService directoryService, IAppConfigService appConfigService, IHostEnvironment hostingEnvironment)
		{
			_directoryService = directoryService;
			_appConfigService = appConfigService;
			_hostingEnvironment = hostingEnvironment;
		}

		/// <inheritdoc cref="IAppFolderService.Create"/>
		public Task Create()
		{
			// If environment dev => path == parentfolder
			// unix == "/"
			// windows "appdata/local" = > dev ../Smarthub.ConfigFolder-dev
			// Use DoNotVerify in case Folder doesn’t exist.
			var (path, baseFolderName) = GetHomeFolderPath();
			if (string.IsNullOrEmpty(path))
			{
				return Task.CompletedTask;
			}
			var appConfig = _appConfigService.GetConfig();
			_directoryService.CreateDirectory(path, baseFolderName);
			var homePath = Path.Combine(path, baseFolderName);
			CreateConfigFolder(appConfig, homePath);
			CreatePluginFolder(appConfig, homePath);
			CreateLogsFolder(appConfig, homePath);
			_appConfigService.CreateOrGetConfigFile();
			return Task.CompletedTask;
		}

		/// <inheritdoc cref="IAppFolderService.Save"/>
		public Task Save()
		{
			_appConfigService.SaveConfigAsync();
			return Task.CompletedTask;
		}

		/// <inheritdoc cref="IAppFolderService.GetHomeFolderPath"/>
		public Tuple<string, string> GetHomeFolderPath()
		{
			return _hostingEnvironment.EnvironmentName != "Development"
				? GetSystemHomeFolderLocation()
				: GetDevEnvironmentFolderLocation();
		}

		private void CreatePluginFolder(AppConfig appConfig, string homePath)
		{
			var pluginPath = Path.Combine(homePath, appConfig.PluginFolderName ?? string.Empty);
			_directoryService.CreateDirectory(pluginPath);
			appConfig.PluginFolderPath = pluginPath;
		}

		private void CreateLogsFolder(AppConfig appConfig, string homePath)
		{
			var logPath = Path.Combine(homePath, appConfig.LogFolderName ?? string.Empty);
			_directoryService.CreateDirectory(logPath);
			appConfig.LogFolderPath = logPath;
		}

		private void CreateConfigFolder(AppConfig appConfig, string homePath)
		{
			var configPath = Path.Combine(homePath, appConfig.ConfigFolderName ?? string.Empty);
			_directoryService.CreateDirectory(configPath);
			appConfig.ConfigFolderPath = configPath;
		}

		private Tuple<string, string> GetSystemHomeFolderLocation()
		{
			var appConfig = _appConfigService.GetConfig();
			return new Tuple<string, string>(
				(Environment.OSVersion.Platform == PlatformID.Unix ||
				 Environment.OSVersion.Platform == PlatformID.MacOSX
					? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).IsNullOrEmpty()
						? Environment.GetEnvironmentVariable("HOME")
						: Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments,
							Environment.SpecialFolderOption.DoNotVerify)
					: Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData,
						Environment.SpecialFolderOption.DoNotVerify))
				?? throw new SmartHubException("Could not find system path"),
				appConfig.BaseFolderName ?? string.Empty);
		}

		private Tuple<string, string> GetDevEnvironmentFolderLocation()
		{
			var appConfig = _appConfigService.GetConfig();
			return new Tuple<string, string>(Path.Combine(
					Path.GetDirectoryName(
						Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory()))) ??
					throw new SmartHubException("Could not find development system path")),
				appConfig.BaseFolderName + ".configFolder-dev");
		}
	}
}