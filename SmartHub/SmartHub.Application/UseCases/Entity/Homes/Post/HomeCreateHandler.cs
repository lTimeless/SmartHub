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

namespace SmartHub.Application.UseCases.Entity.Homes.Post
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand, Response<HomeDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly CurrentUser _currentUser;
		private readonly ILocationService _locationService;
		private readonly IOptionsMonitor<ApplicationSettings> _optionsSnapshot;
		private readonly ILogger _logger = Log.ForContext(typeof(HomeCreateHandler));

		public HomeCreateHandler(IUnitOfWork unitOfWork, IOptionsMonitor<ApplicationSettings> optionsSnapshot, CurrentUser currentUser, ILocationService locationService, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_optionsSnapshot = optionsSnapshot;
			_currentUser = currentUser;
			_locationService = locationService;
			_mapper = mapper;
		}

		public async Task<Response<HomeDto>> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			if (home != null)
			{
				return Response.Fail<HomeDto>("Error: There is already a home.");
			}

			var defaultSetting = new Setting($"{request.Name}_Setting_default",
				"This is a default setting",
				true,
				_optionsSnapshot.CurrentValue.DefaultPluginPath,
				_optionsSnapshot.CurrentValue.DownloadServerUrl,
				_currentUser.RequesterName,
				SettingTypes.Default);
			var homeEntity = new Home(request.Name, request.Description)
				.AddSetting(defaultSetting);

			if (request.AutoDetectAddress)
			{
				var locationDto = await _locationService.GetLocation();
				if (locationDto != null)
				{
					homeEntity.AddAddress(locationDto.City, locationDto.Region, locationDto.Country, locationDto.ZipCode);
				}
			}

			var result = await _unitOfWork.HomeRepository.AddAsync(homeEntity);
			if (!result)
			{
				return Response.Fail<HomeDto>("Error: Could not create Home.");
			}
			_logger.Information("SmartHub successfully created.");
			return Response.Ok("SmartHub successfully created.", _mapper.Map<HomeDto>(homeEntity));
		}
	}
}
