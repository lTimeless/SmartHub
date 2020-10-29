using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Serilog;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Settings;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand, Response<HomeDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILocationService _locationService;
		private readonly IOptionsMonitor<ApplicationSettings> _optionsSnapshot;
		private readonly ILogger _logger = Log.ForContext(typeof(HomeCreateHandler));

		public HomeCreateHandler(IUnitOfWork unitOfWork, IOptionsMonitor<ApplicationSettings> optionsSnapshot, ILocationService locationService, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_optionsSnapshot = optionsSnapshot;
			_locationService = locationService;
			_mapper = mapper;
		}

		public async Task<Response<HomeDto>> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			if (home != null)
			{
				return Response.Fail("Error: There is already a home.", new HomeDto());
			}

			var defaultSetting = new Setting($"{request.Name}_{DefaultNames.DefaultSetting}",
				"This is a default setting",
				true,
				_optionsSnapshot.CurrentValue.DefaultPluginPath ?? string.Empty,
				_optionsSnapshot.CurrentValue.DownloadServerUrl ?? string.Empty,
				SettingTypes.Default);

			var defaultGroup = new Group(DefaultNames.DefaultGroup, "Default_Description");
			var homeEntity = new Home(request.Name, request.Description)
				.AddSetting(defaultSetting)
				.AddGroup(defaultGroup);

			if (request.AutoDetectAddress)
			{
				var locationDto = await _locationService.GetLocation();
				if (locationDto != null)
				{
					homeEntity.AddAddress(
						locationDto.City ?? string.Empty,
						locationDto.Region ?? string.Empty,
						locationDto.Country ?? string.Empty,
						locationDto.ZipCode ?? string.Empty);
				}
			}

			var result = await _unitOfWork.HomeRepository.AddAsync(homeEntity);
			if (!result)
			{
				return Response.Fail("Error: Could not create Home.", new HomeDto());
			}
			_logger.Information("SmartHub successfully created.");
			return Response.Ok("SmartHub successfully created.", _mapper.Map<HomeDto>(homeEntity));
		}
	}
}
