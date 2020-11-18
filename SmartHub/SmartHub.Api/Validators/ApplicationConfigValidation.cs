using Microsoft.Extensions.Options;
using SmartHub.Domain.Common;

namespace SmartHub.Api.Validators
{
    public class ApplicationConfigValidation : IValidateOptions<ApplicationConfig>
    {
        public ValidateOptionsResult Validate(string name, ApplicationConfig options)
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