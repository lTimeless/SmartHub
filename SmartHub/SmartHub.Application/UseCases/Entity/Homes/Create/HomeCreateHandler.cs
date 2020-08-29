using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Serilog;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Settings;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand, bool>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly CurrentUser _currentUser;
		private readonly ILocationService _locationService;
		private readonly IOptionsMonitor<ApplicationSettings> _optionsSnapshot;
		private readonly ILogger _logger = Log.ForContext(typeof(HomeCreateHandler));

		public HomeCreateHandler(IUnitOfWork unitOfWork, IOptionsMonitor<ApplicationSettings> optionsSnapshot, CurrentUser currentUser, ILocationService locationService)
		{
			_unitOfWork = unitOfWork;
			_optionsSnapshot = optionsSnapshot;
			_currentUser = currentUser;
			_locationService = locationService;
		}

		public async Task<bool> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			var homAlreadyExists = await _unitOfWork.HomeRepository.GetHome();
			if (homAlreadyExists != null)
			{
				_logger.Information("There is already a home.");
				return false;
			}

			var defaultSetting = new Setting($"{request.Name}_Setting_default", "This is a default setting", true,
				_optionsSnapshot.CurrentValue.DefaultPluginpath, _optionsSnapshot.CurrentValue.DefaultPluginpath,
				_optionsSnapshot.CurrentValue.DownloadServerUrl, _currentUser.RequesterName, SettingTypes.Default);
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
				_logger.Information("Could not create Home.");
				return false;
			}
			_logger.Information("SmartHub created.");
			return true;
		}
	}
}
