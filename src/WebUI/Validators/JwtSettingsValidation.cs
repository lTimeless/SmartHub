using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Helpers;
using SmartHub.Domain.Common.Settings;

namespace SmartHub.WebUI.Validators
{
    public class JwtSettingsValidation : IValidateOptions<JwtSettings>
    {
        public ValidateOptionsResult Validate(string name, JwtSettings options)
        {
            if (string.IsNullOrEmpty(options.Key) || options.Key.Contains("<"))
            {
                options.Key = TokenUtils.ValidateAndGenerateToken(options.Key ?? string.Empty);
            }
            return ValidateOptionsResult.Success;
        }
    }
}