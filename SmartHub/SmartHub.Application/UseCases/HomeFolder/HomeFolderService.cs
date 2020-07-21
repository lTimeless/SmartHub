using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Common.Settings;

namespace SmartHub.Application.UseCases.HomeFolder
{
    public class HomeFolderService : IHomeFolderService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IOptionsMonitor<ApplicationSettings> _applicationSettings;

        // The overlaying service for creating the SmartHub config folder
        // with functions to create, delete and update it
        public HomeFolderService(IDirectoryService directoryService, IOptionsMonitor<ApplicationSettings> applicationSettings)
        {
            _directoryService = directoryService;
            _applicationSettings = applicationSettings;
        }
        public Task Init()
        {
            // If environment dev => path == parentfolder
            // unix == "/"
            // windows "appdata/local" = > dev ../Smarthub.ConfigFolder-dev

            // Use DoNotVerify in case Folder doesn’t exist.
            var ( homePath, folderName ) = GetHomeFolderPath();

            if (!string.IsNullOrEmpty(homePath))
            {
                var pluginPath = Path.Combine(homePath, folderName);
                _applicationSettings.CurrentValue.DefaultPluginpath = pluginPath; // TODO: add plugins folder
                _directoryService.CreateDirectory(homePath, folderName);
                Log.Information("[HomeFolderService] SmartHub folder is at {@homePath}\\{@folderName}",
                    homePath,
                    folderName);
                CreatePluginFolderInHomeFolder();
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        private void CreatePluginFolderInHomeFolder()
        {
            var (path, folderName) = GetHomeFolderPath();
            var homePath = Path.Combine(path, folderName);
            var pluginPath = Path.Combine(homePath, _applicationSettings.CurrentValue.DefaultPluginFolderName);
            _directoryService.CreateDirectory(pluginPath);
            _applicationSettings.CurrentValue.DefaultPluginpath = pluginPath;
        }

        public Tuple<string, string> GetHomeFolderPath()
        {
            return  _applicationSettings.CurrentValue.EnvironmentName != "Development"
                ? GetSystemHomeFolderLocation()
                : GetDevEnvironmentFolderLocation();
        }

        private Tuple<string, string> GetSystemHomeFolderLocation()
        {
            return new Tuple<string, string>((Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX
                ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).IsNullOrEmpty()
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.DoNotVerify)
                : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.DoNotVerify))
                   ?? throw new SmartHubException("Could not find system path"),
                _applicationSettings.CurrentValue.FolderName);
        }

        private Tuple<string, string> GetDevEnvironmentFolderLocation()
        {
            return new Tuple<string, string>(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory()))) ??
                throw new SmartHubException("Could not find development system path")),
                _applicationSettings.CurrentValue.FolderName+".configFolder-dev");
        }
    }
}