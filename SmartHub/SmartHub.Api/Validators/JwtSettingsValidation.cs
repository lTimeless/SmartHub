using Microsoft.Extensions.Options;
using SmartHub.Domain.Common.Settings;
using SmartHub.Infrastructure.Helpers;

namespace SmartHub.Api.Validators
{
    public class JwtSettingsValidation : IValidateOptions<JwtSettings>
    {
        public ValidateOptionsResult Validate(string name, JwtSettings options)
        {
            if (string.IsNullOrEmpty(options.Key) || options.Key.Contains("<"))
            {
                options.Key = TokenUtils.ValidateAndGenerateToken(options.Key);
            }
            return ValidateOptionsResult.Success;
        }
    }
}