using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Serilog;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Domain;
using SmartHub.Domain.Common.Extensions;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand, Response<AppDto>>
	{
		private readonly IConfigService _configService;
		private readonly IMapper _mapper;
		private readonly ILocationService _locationService;
		private readonly IOptionsMonitor<AppConfig> _appConfig;
		private readonly ILogger _logger = Log.ForContext(typeof(HomeCreateHandler));

		public HomeCreateHandler(IOptionsMonitor<Domain.AppConfig> appConfig, ILocationService locationService, IMapper mapper, IConfigService configService)
		{
			_appConfig = appConfig;
			_locationService = locationService;
			_mapper = mapper;
			_configService = configService;
		}

		public async Task<Response<AppDto>> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			if (_appConfig.CurrentValue.IsActive)
			{
				return Response.Fail("Error: There is already a home.", new AppDto());
			}

			if (request.AutoDetectAddress)
			{
				var locationDto = await _locationService.GetLocation();
				if (locationDto != null)
				{
					_appConfig.CurrentValue.AddAddress(
						locationDto.City ?? string.Empty,
						locationDto.Region ?? string.Empty,
						locationDto.Country ?? string.Empty,
						locationDto.ZipCode ?? string.Empty);
				}
			}

			_appConfig.CurrentValue.IsActive = true;

			var result = _configService.UpdateFileFromClass(); ;
			if (!result)
			{
				return Response.Fail("Error: Could not create Home.", new AppDto());
			}
			_logger.Information("SmartHub successfully created.");
			return Response.Ok("SmartHub successfully created.", _mapper.Map<AppDto>(_appConfig.CurrentValue));
		}
	}
}
