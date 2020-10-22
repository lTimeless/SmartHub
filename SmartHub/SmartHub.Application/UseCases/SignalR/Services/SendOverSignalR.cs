using System;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.Entity.Homes;
using System.Threading.Tasks;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Activities;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.SignalR.Services
{
	/// <inheritdoc cref="ISendOverSignalR"/>
	public class SendOverSignalR : ISendOverSignalR
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IHubContext<HomeHub, IServerHub> _homeHubContext;
		private readonly IHubContext<ActivityHub, IServerHub> _activityHubContext;

		public SendOverSignalR(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<HomeHub, IServerHub> homeHubContext, IHubContext<ActivityHub, IServerHub> activityHubContext)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_homeHubContext = homeHubContext;
			_activityHubContext = activityHubContext;
		}

		/// <inheritdoc cref="ISendOverSignalR.SendHome"/>
		public async Task SendHome()
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			await _homeHubContext.Clients.All.SendHome(_mapper.Map<HomeDto>(home));
		}

		/// <inheritdoc cref="ISendOverSignalR.SendActivity"/>
		public async Task SendActivity(string userName,string requestName, string message, long execTime,bool success)
		{
			//das failt hier da am anfang eine activit erstellt wird und am ende aber beide ohen ID
			// da erst am ganz ende eines requests SaveDb gemacht wird und dann erst die DB  die IDs erstellt
			var activityDto = new ActivityDto
			{
				DateTime = DateTime.Now.ToString("HH:mm:ss"),
				Username = userName,
				Message = message,
				ExecutionTime = execTime,
				SuccessfulRequest = success
			};
			await _activityHubContext.Clients.All.SendActivity(activityDto);
			var home = await _unitOfWork.HomeRepository.GetHome();
			var activity = _mapper.Map<Activity>(activityDto);
			activity.UpdateName(requestName + "_" + activityDto.DateTime);
			home.AddActivity(activity);
		}
	}
}
