using AutoMapper;
using MediatR;
using SmartHub.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Settings;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand, Response<HomeDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Random _random;
		private readonly CurrentUser _currentUser;
		private readonly IOptionsMonitor<ApplicationSettings> _optionsSnapshot;

		public HomeCreateHandler(IMapper mapper, IUnitOfWork unitOfWork, IOptionsMonitor<ApplicationSettings> optionsSnapshot, CurrentUser currentUser)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_optionsSnapshot = optionsSnapshot;
			_currentUser = currentUser;
			_random = new Random();
		}

		public async Task<Response<HomeDto>> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			var homAlreadyExists = await _unitOfWork.HomeRepository.GetHome();
			if (homAlreadyExists != null)
			{
				return Response.Fail<HomeDto>($"[{nameof(HomeCreateHandler)}] There is already a home");
			}

			if (_currentUser.User is null)
			{
				return Response.Fail<HomeDto>( $"[{nameof(HomeCreateHandler)}] There is no User logged in");
			}

			var defaultSetting = new Setting($"default_Setting_{_random.Next()}", "this is a default setting", true,
				_optionsSnapshot.CurrentValue.DefaultPluginpath, _optionsSnapshot.CurrentValue.DefaultPluginpath,
				_optionsSnapshot.CurrentValue.DownloadServerUrl, _currentUser.User.UserName, SettingTypes.Default);

			var homeEntity = new Home(request.Name, request.Description, defaultSetting);
			homeEntity.AddUser(_currentUser.User);

			var result = await _unitOfWork.HomeRepository.AddAsync(homeEntity);
			if (!result)
			{
				return Response.Fail<HomeDto>( $"[{nameof(Handle)}] Could not create Home");
			}
			await _unitOfWork.SaveAsync();

			return Response.Ok("Created new Home", _mapper.Map<HomeDto>(homeEntity));
		}
	}
}
