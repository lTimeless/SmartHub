using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.Entity.Activities;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Groups;
using SmartHub.Domain;
using SmartHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.SignalR.Services
{
	/// <inheritdoc cref="ISendOverSignalR"/>
	public class SendOverSignalR : ISendOverSignalR
	{
		private readonly IBaseRepositoryAsync<Activity> _activityRepository;
		private readonly IBaseRepositoryAsync<Group> _groupRepository;
		private readonly IBaseRepositoryAsync<Device> _deviceRepository;

		private readonly IMapper _mapper;
		private readonly IHubContext<ActivityHub, IServerHubClient> _activityHubContext;
		private readonly IHubContext<HomeHub, IServerHubClient> _homeHubContext;
		private readonly IAppConfigService _appConfigService;

		public SendOverSignalR(IMapper mapper, IHubContext<ActivityHub, IServerHubClient> activityHubContext, 
			IBaseRepositoryAsync<Activity> activityRepository, IHubContext<HomeHub, IServerHubClient> homeHubContext, 
			IBaseRepositoryAsync<Group> groupRepository, IBaseRepositoryAsync<Device> deviceRepository, IAppConfigService appConfigService)
		{
			_mapper = mapper;
			_activityHubContext = activityHubContext;
			_activityRepository = activityRepository;
			_homeHubContext = homeHubContext;
			_groupRepository = groupRepository;
			_deviceRepository = deviceRepository;
			_appConfigService = appConfigService;
		}

		/// <inheritdoc cref="ISendOverSignalR.SendActivity"/>
		public async Task SendActivity(string userName, string requestName, string message, long execTime, bool success)
		{
			var appConfig = _appConfigService.GetConfig();
			if (appConfig.IsActive is false)
			{
				return;
			}

			var activityDto = new ActivityDto(DateTime.Now.ToString("HH:mm:ss"), userName, message, execTime, success, requestName);
			await _activityHubContext.Clients.All.SendActivity(activityDto);

			var activity = _mapper.Map<Activity>(activityDto);

			var addedSuccessful = await _activityRepository.AddAsync(activity);
			if (!addedSuccessful)
			{
				throw new SmartHubException("Error adding new activity to database");
			}
			var activities = await _activityRepository.GetAllAsync();

			if (activities.Count > appConfig.SaveXLimit)
			{
				if (appConfig.SaveXLimit != null && appConfig.DeleteXAmountAfterLimit != null)
				{
					var amount = activities.Count;
					var deleteXAmount = amount - (int)appConfig.SaveXLimit;
					activities.RemoveAll(a => activities
						.OrderByDescending(x => x.CreatedAt)
						.TakeLast(deleteXAmount)
						.ToList()
						.Exists(la => la.Id == a.Id));
				}
			}

		}

		public async Task SendAppConfig()
		{
			await _homeHubContext.Clients.All.SendAppConfig(_appConfigService.GetConfig());
		}

		public async Task SendDevices()
		{
			var devices = await _deviceRepository.GetAllAsync();
			await _homeHubContext.Clients.All.SendDevices(_mapper.Map<IEnumerable<DeviceDto>>(devices));
		}

		public async Task SendGroups()
		{
			var groups = await _groupRepository.GetAllAsync();
			await _homeHubContext.Clients.All.SendGroups(_mapper.Map<IEnumerable<GroupDto>>(groups));
		}
	}
}
