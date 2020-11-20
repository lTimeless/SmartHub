using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.Entity.Activities;
using SmartHub.Domain;
using SmartHub.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.SignalR.Services
{
	/// <inheritdoc cref="ISendOverSignalR"/>
	public class SendOverSignalR : ISendOverSignalR
	{
		private readonly IBaseRepositoryAsync<Activity> _activityRepository;
		private readonly IMapper _mapper;
		private readonly IHubContext<ActivityHub, IServerHub> _activityHubContext;
		private readonly IOptionsSnapshot<AppConfig> _appConfig;

		public SendOverSignalR(IMapper mapper,IHubContext<ActivityHub, IServerHub> activityHubContext, 
			IOptionsSnapshot<AppConfig> homeConfig, IBaseRepositoryAsync<Activity> activityRepository)
		{
			_mapper = mapper;
			_activityHubContext = activityHubContext;
			_appConfig = homeConfig;
			_activityRepository = activityRepository;
		}

		/// <inheritdoc cref="ISendOverSignalR.SendActivity"/>
		public async Task SendActivity(string userName, string requestName, string message, long execTime, bool success)
		{

			if (_appConfig.Value.IsActive is false)
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

			if (activities.Count > _appConfig.Value.SaveXLimit)
			{
				if (_appConfig.Value.SaveXLimit != null && _appConfig.Value.DeleteXAmountAfterLimit != null)
				{
					var amount = activities.Count;
					var deleteXAmount = amount - (int)_appConfig.Value.SaveXLimit;
					activities.RemoveAll(a => activities
						.OrderByDescending(x => x.CreatedAt)
						.TakeLast(deleteXAmount)
						.ToList()
						.Exists(la => la.Id == a.Id));
				}
			}

		}
	}
}
