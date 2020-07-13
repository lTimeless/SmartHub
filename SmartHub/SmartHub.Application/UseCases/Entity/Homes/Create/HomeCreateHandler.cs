using AutoMapper;
using MediatR;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Entities.Settings;
using SmartHub.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Settings;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand, ServiceResponse<HomeDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Random _random;
		private readonly CurrentUser _currentUser;
		private readonly IOptionsSnapshot<ApplicationSettings> _optionsSnapshot;

		public HomeCreateHandler(IMapper mapper, IUnitOfWork unitOfWork, IOptionsSnapshot<ApplicationSettings> optionsSnapshot, CurrentUser currentUser)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_optionsSnapshot = optionsSnapshot;
			_currentUser = currentUser;
			_random = new Random();
		}

		public async Task<ServiceResponse<HomeDto>> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			var homAlreadyExists = await _unitOfWork.HomeRepository.GetHome();
			if (homAlreadyExists != null)
			{
				return new ServiceResponse<HomeDto>(false, $"[{nameof(HomeCreateHandler)}] There is already a home");
			}

			if (_currentUser.User is null)
			{
				return new ServiceResponse<HomeDto>(false, $"[{nameof(HomeCreateHandler)}] There is no User logged in");
			}

			var defaultSetting = new Setting($"default_Setting_{_random.Next()}", "this is a default setting", true,
				_optionsSnapshot.Value.DefaultPluginpath, _optionsSnapshot.Value.DefaultPluginpath,
				_optionsSnapshot.Value.DownloadServerUrl, _currentUser.User.UserName, SettingTypeEnum.Default);

			var homeEntity = new Home(request.Name, request.Description, defaultSetting);
			homeEntity.AddUser(_currentUser.User);

			var result = await _unitOfWork.HomeRepository.AddAsync(homeEntity);
			if (!result)
			{
				return new ServiceResponse<HomeDto>(false, $"[{nameof(Handle)}] Could not create Home");
			}
			await _unitOfWork.SaveAsync();

			var homeResponseDto = _mapper.Map<HomeDto>(homeEntity);
			return new ServiceResponse<HomeDto>(homeResponseDto, true);
		}
	}
}
