using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Settings;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly Random _random;
		private readonly CurrentUser _currentUser;
		private readonly IOptionsMonitor<ApplicationSettings> _optionsSnapshot;

		// TODO: hier vlt mediatr notification anstatt request
		public HomeCreateHandler(IUnitOfWork unitOfWork, IOptionsMonitor<ApplicationSettings> optionsSnapshot, CurrentUser currentUser)
		{
			_unitOfWork = unitOfWork;
			_optionsSnapshot = optionsSnapshot;
			_currentUser = currentUser;
			_random = new Random();
		}

		public async Task<Unit> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			var homAlreadyExists = await _unitOfWork.HomeRepository.GetHome();
			if (homAlreadyExists != null)
			{
				throw new SmartHubException($"[{nameof(HomeCreateHandler)}] There is already a home.");
			}

			var defaultSetting = new Setting($"default_Setting_{_random.Next()}", "this is a default setting", true,
				_optionsSnapshot.CurrentValue.DefaultPluginpath, _optionsSnapshot.CurrentValue.DefaultPluginpath,
				_optionsSnapshot.CurrentValue.DownloadServerUrl, _currentUser.RequesterName, SettingTypes.Default);
			// TODO: alle weitere felder füllen wie Addresse
			var homeEntity = new Home(request.Name, request.Description, defaultSetting);

			var result = await _unitOfWork.HomeRepository.AddAsync(homeEntity);
			if (!result)
			{
				throw new SmartHubException( $"[{nameof(Handle)}] Could not create Home.");
			}
			return Unit.Value;
		}
	}
}
