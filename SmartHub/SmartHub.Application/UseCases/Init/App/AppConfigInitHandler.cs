using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Domain;
using SmartHub.Domain.Common.Extensions;

namespace SmartHub.Application.UseCases.Init.App
{
	public class AppConfigInitHandler : IRequestHandler<AppConfigInitCommand, Response<AppConfig>>
	{
		private readonly IAppConfigService _configService;
		private readonly IMapper _mapper;
		private readonly ILocationService _locationService;
		private readonly ILogger _logger = Log.ForContext(typeof(AppConfigInitHandler));

		public AppConfigInitHandler( ILocationService locationService, IMapper mapper, IAppConfigService configService)
		{
			_locationService = locationService;
			_mapper = mapper;
			_configService = configService;
		}

		public async Task<Response<AppConfig>> Handle(AppConfigInitCommand request, CancellationToken cancellationToken)
		{
			var appConfig = _configService.GetConfig();

			if (appConfig.IsActive)
			{
				return Response.Fail("Error: There is already a home.", new AppConfig());
			}

			if (request.AutoDetectAddress)
			{
				var locationDto = await _locationService.GetLocation();
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

			await _configService.UpdateConfig(appConfig);
			_logger.Information("SmartHub successfully created.");
			return Response.Ok("SmartHub successfully created.", _mapper.Map<AppConfig>(appConfig));
		}
	}
}
