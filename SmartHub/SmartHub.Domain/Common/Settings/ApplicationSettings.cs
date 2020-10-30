
namespace SmartHub.Domain.Common.Settings
{
    public class ApplicationSettings
    {
        public string? DownloadServerUrl { get; set; }
        public string? DefaultPluginPath { get; set; }
        public string? FolderName { get; set; }
        public string? ApplicationName { get; set; }
        public string? PluginFolderName { get; set; }
        public string? LogFolderName { get; set; }
        public string? LogFilePath { get; set; }
        public int? DeleteXAmountAfterLimit { get; set; }
        public int? SaveXLimit { get; set; }
    }
}