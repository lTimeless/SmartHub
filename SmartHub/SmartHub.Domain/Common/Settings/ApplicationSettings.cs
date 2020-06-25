using EnvironmentName = Microsoft.Extensions.Hosting.EnvironmentName;

namespace SmartHub.Domain.Common.Settings
{
    public class ApplicationSettings
    {
        public string DownloadServerUrl { get; set; }
        public string DefaultPluginpath { get; set; }
        public string EnvironmentName { get; set; }
        public string FolderName { get; set; }
        public string DefaultPluginFolderName { get; set; }
        public string DefaultName { get; set; }

    }
}