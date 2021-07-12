using HotChocolate;
using Serilog;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Domain.Common.Constants;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Extensions;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Init
{
	/// <summary>
	///     Endpoint for initialization.
	/// </summary>
	[GraphQLDescription("Entrypoint for initialization services.")]
	public class InitMutations
	{
		public async Task<InitPayload> InitializeApp(AppConfigInitInput input,
			[Service] IAppConfigService appConfigService,
			[Service] ILocationService locationService,
			[Service] ILogger logger)
		{
			var appConfig = appConfigService.GetConfig();
			if (appConfig.IsActive)
			{
				return new(
					new("There is already an active home.", AppErrorCodes.Exists));
			}

			if (input.AutoDetectAddress)
			{
				var locationDto = await locationService.GetLocation();
				if (locationDto != null)
				{
					appConfig.AddAddress(
						locationDto.City ?? string.Empty,
						locationDto.Region ?? string.Empty,
						locationDto.Country ?? string.Empty,
						locationDto.ZipCode ?? string.Empty);
				}
			}

			appConfig.ApplicationName = !string.IsNullOrWhiteSpace(input.Name) ? input.Name : DefaultNames.AppName;
			appConfig.Description = !string.IsNullOrWhiteSpace(input.Description)
				? input.Description
				: DefaultNames.AppDescription;

			appConfig.IsActive = true;
			await appConfigService.UpdateConfigAsync(appConfig);
			logger.Information("SmartHub successfully created.");
			return new(appConfig, "SmartHub successfully created.");
		}
	}
}