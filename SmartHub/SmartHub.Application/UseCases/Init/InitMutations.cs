// unset

using HotChocolate;
using Serilog;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Domain;
using SmartHub.Domain.Common.Extensions;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Init
{
	public class InitMutations
	{
		public async Task<Response<AppConfig>> InitApp(AppConfigInitInput input,
			[Service] IAppConfigService appConfigService,
			[Service] ILocationService locationService,
			[Service] ILogger logger)
		{
			var appConfig = appConfigService.GetConfig();
			if (appConfig.IsActive)
			{
				return Response.Fail("Error: There is already a home.", new AppConfig());
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
			appConfig.IsActive = true;

			await appConfigService.UpdateConfig(appConfig);
			logger.Information("SmartHub successfully created.");
			return Response.Ok("SmartHub successfully created.", appConfig);
		}
	}
}