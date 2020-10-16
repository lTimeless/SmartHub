using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.Entity.Homes;
using System.Threading.Tasks;
using SmartHub.Application.Common.Models;

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
		public async Task SendActivity(Activity activity)
		{
			await _activityHubContext.Clients.All.SendActivity(activity);
			// Here also send it to the database
			// unit of Work will trigger after this(if this is executed from the ""RequestLoggerBehavior)
		}
	}
}
