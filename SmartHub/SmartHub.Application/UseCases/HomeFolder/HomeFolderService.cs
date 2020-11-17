using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.HomeFolder
{
	/// <inheritdoc cref="IHomeFolderService"/>
	public class HomeFolderService : IHomeFolderService
	{
		private readonly IDirectoryService _directoryService;
		private readonly IOptionsMonitor<HomeConfiguration> _homeConfig;
		private readonly IHostEnvironment _hostingEnvironment;

		// The overlaying service for creating the SmartHub config folder
		// with functions to create, delete and update it
		public HomeFolderService(IDirectoryService directoryService,
			IOptionsMonitor<HomeConfiguration> homeConfig, IHostEnvironment hostingEnvironment)
		{
			_directoryService = directoryService;
			_homeConfig = homeConfig;
			_hostingEnvironment = hostingEnvironment;
		}

		/// <inheritdoc cref="IHomeFolderService.Create"/>
		public Task Create()
		{
			// If environment dev => path == parentfolder
			// unix == "/"
			// windows "appdata/local" = > dev ../Smarthub.ConfigFolder-dev
			// Use DoNotVerify in case Folder doesn’t exist.
			var ( homePath, baseFolderName ) = GetHomeFolderPath();
			if (string.IsNullOrEmpty(homePath))
			{
				return Task.CompletedTask;
			}
			var pluginPath = Path.Combine(homePath, baseFolderName);
			_homeConfig.CurrentValue.PluginFolderPath = pluginPath;
			_directoryService.CreateDirectory(homePath, baseFolderName);
			CreateConfigFolder();
			CreatePluginFolder();
			CreateLogsFolder();
			return Task.CompletedTask;

		}

		/// <inheritdoc cref="IHomeFolderService.GetHomeFolderPath"/>
		public Tuple<string, string> GetHomeFolderPath()
		{
			return _hostingEnvironment.EnvironmentName != "Development"
				? GetSystemHomeFolderLocation()
				: GetDevEnvironmentFolderLocation();
		}

		private void CreatePluginFolder()
		{
			var (path, folderName) = GetHomeFolderPath();
			var homePath = Path.Combine(path, folderName);
			var pluginPath = Path.Combine(homePath, _homeConfig.CurrentValue.PluginFolderName ?? string.Empty);
			_directoryService.CreateDirectory(pluginPath);
			_homeConfig.CurrentValue.PluginFolderPath = pluginPath;
		}

		private void CreateLogsFolder()
		{
			var (path, folderName) = GetHomeFolderPath();
			var homePath = Path.Combine(path, folderName);
			var logPath = Path.Combine(homePath, _homeConfig.CurrentValue.LogFolderName ?? string.Empty);
			_directoryService.CreateDirectory(logPath);
			_homeConfig.CurrentValue.LogFolderPath = logPath;
		}

		private void CreateConfigFolder()
		{
			var (path, folderName) = GetHomeFolderPath();
			var homePath = Path.Combine(path, folderName);
			var configPath = Path.Combine(homePath, _homeConfig.CurrentValue.ConfigFolderName ?? string.Empty);
			_directoryService.CreateDirectory(configPath);
			_homeConfig.CurrentValue.ConfigFolderPath = configPath;
		}

		private Tuple<string, string> GetSystemHomeFolderLocation()
		{
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
				_homeConfig.CurrentValue.BaseFolderName ?? string.Empty);
		}

		private Tuple<string, string> GetDevEnvironmentFolderLocation()
		{
			return new Tuple<string, string>(Path.Combine(
					Path.GetDirectoryName(
						Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory()))) ??
					throw new SmartHubException("Could not find development system path")),
				_homeConfig.CurrentValue.BaseFolderName + ".configFolder-dev");
		}
	}
}