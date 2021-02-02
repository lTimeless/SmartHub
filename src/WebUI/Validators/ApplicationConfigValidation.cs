using Microsoft.Extensions.Options;
using SmartHub.Domain;

namespace SmartHub.WebUI.Validators
{
	public class ApplicationConfigValidation : IValidateOptions<AppConfig>
    {
        public ValidateOptionsResult Validate(string name, AppConfig options)
        {
            if (string.IsNullOrEmpty(options.BaseFolderName))
            {
                return ValidateOptionsResult.Fail("SmartHub:FolderName must be defined in the appsettings.json file");
            }
            if (string.IsNullOrEmpty(options.PluginFolderName))
            {
                return ValidateOptionsResult.Fail("SmartHub:DefaultPluginFolderName must be defined in the appsettings.json file");

            }
            if (string.IsNullOrEmpty(options.ApplicationName))
            {
                return ValidateOptionsResult.Fail("SmartHub:ApplicationName must be defined in the appsettings.json file");

            }
            return ValidateOptionsResult.Success;
        }
    }
}