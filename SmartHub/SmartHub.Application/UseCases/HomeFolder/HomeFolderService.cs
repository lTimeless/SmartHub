using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Common.Settings;

namespace SmartHub.Application.UseCases.HomeFolder
{
    /// <inheritdoc cref="IHomeFolderService"/>
    public class HomeFolderService : IHomeFolderService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IOptionsMonitor<ApplicationSettings> _applicationSettings;
        private readonly IHostingEnvironment _hostingEnvironment;

        // The overlaying service for creating the SmartHub config folder
        // with functions to create, delete and update it
        public HomeFolderService(IDirectoryService directoryService,
            IOptionsMonitor<ApplicationSettings> applicationSettings, IHostingEnvironment hostingEnvironment)
        {
            _directoryService = directoryService;
            _applicationSettings = applicationSettings;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <inheritdoc cref="IHomeFolderService.Create"/>
        public Task Create()
        {
            // If environment dev => path == parentfolder
            // unix == "/"
            // windows "appdata/local" = > dev ../Smarthub.ConfigFolder-dev
            // Use DoNotVerify in case Folder doesn’t exist.
            var ( homePath, folderName ) = GetHomeFolderPath();
            if (string.IsNullOrEmpty(homePath))
            {
                return Task.CompletedTask;
            }
            var pluginPath = Path.Combine(homePath, folderName);
            _applicationSettings.CurrentValue.DefaultPluginPath = pluginPath;
            _directoryService.CreateDirectory(homePath, folderName);
            CreatePluginFolderInHomeFolder();
            CreateLogsFolderInHomeFolder();
            return Task.CompletedTask;

        }

        /// <inheritdoc cref="IHomeFolderService.GetHomeFolderPath"/>
        public Tuple<string, string> GetHomeFolderPath()
        {
            return _hostingEnvironment.EnvironmentName != "Development"
                ? GetSystemHomeFolderLocation()
                : GetDevEnvironmentFolderLocation();
        }

        private void CreatePluginFolderInHomeFolder()
        {
            var (path, folderName) = GetHomeFolderPath();
            var homePath = Path.Combine(path, folderName);
            var pluginPath = Path.Combine(homePath, _applicationSettings.CurrentValue.PluginFolderName);
            _directoryService.CreateDirectory(pluginPath);
            _applicationSettings.CurrentValue.DefaultPluginPath = pluginPath;
        }

        private void CreateLogsFolderInHomeFolder()
        {
            var (path, folderName) = GetHomeFolderPath();
            var homePath = Path.Combine(path, folderName);
            var logPath = Path.Combine(homePath, _applicationSettings.CurrentValue.LogFolderName);
            _directoryService.CreateDirectory(logPath);
            _applicationSettings.CurrentValue.LogFilePath = logPath;
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
                _applicationSettings.CurrentValue.FolderName);
        }

        private Tuple<string, string> GetDevEnvironmentFolderLocation()
        {
            return new Tuple<string, string>(Path.Combine(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory()))) ??
                    throw new SmartHubException("Could not find development system path")),
                _applicationSettings.CurrentValue.FolderName + ".configFolder-dev");
        }
    }
}