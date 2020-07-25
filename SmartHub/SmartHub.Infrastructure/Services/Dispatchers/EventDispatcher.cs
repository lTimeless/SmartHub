using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.UseCases.SignalR;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	/// <inheritdoc cref="IEventDispatcher"/>
	public class EventDispatcher : IEventDispatcher
	{
		private readonly IChannelManager _channelManager;
		private readonly ILogger _logger;
		private IDisposable _disposable;
		private readonly IHubContext<EventHub, IServerHub> _hubContext;

		public EventDispatcher(IChannelManager channelManager, ILogger logger, IHubContext<EventHub, IServerHub> hubContext)
		{
			_channelManager = channelManager;
			_logger = logger;
			_hubContext = hubContext;
			_channelManager ??= new ChannelManager();
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			Log.Information("[EventDispatcher] EventDispatcher started in background");
			await Init().ConfigureAwait(false);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			Log.Information("[EventDispatcher] EventDispatcher stopped");
			Dispose();
			return Task.CompletedTask;
		}

		public Task Init()
		{
			_disposable = _channelManager
				.GetChannel(EventTypes.All)
				.Subscribe(Dispatch);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}

		private void Dispatch(IEvent baseEvent)
		{
			if (baseEvent == null) return;
			_hubContext.Clients.All.SendEvent(baseEvent);
			switch (baseEvent.EventType)
			{
				case "Application":
					DispatchApplicationEvent((ApplicationEvent) baseEvent);
					break;
				case "Domain":
				{
					DispatchDomainEvents((DomainEvent) baseEvent);
					break;
				}
				case "Home":
					break;
				case "Http":
					break;
				case "Identity":
					DispatchIdentityEvent((ApplicationEvent) baseEvent);
					break;
				case "All":
					break;
				case "None":
					break;
				default:
					DispatchGeneralEvent(baseEvent);
					break;
			}

		}

		private void DispatchGeneralEvent(IEvent baseEvent)
		{
			_logger.Information("[(DispatchGeneralEvent)] Dispatch Event => {@Name} - {EventType}",
				baseEvent?.GetType().Name, baseEvent.EventType);
			// hier dann alle aus den events Evententitys bauen und in die db speichern
		}

		private void DispatchApplicationEvent(ApplicationEvent applicationEvent)
		{
			switch (applicationEvent)
			{
				default:
					throw new SmartHubException("Unknown event error");
			}
		}

		private void DispatchIdentityEvent(ApplicationEvent applicationEvent)
		{
			switch (applicationEvent)
			{
				case LoginEvent loginEvent:
					var message = $"[DispatchApplicationEvent] Dispatch LoginEvent =>  UserName: {loginEvent.UserName}; Login state: {loginEvent.Successful} ";
					_logger.Information(message);
					break;
				default:
					throw new SmartHubException("Unknown event error");
			}
		}

		private void DispatchDomainEvents(DomainEvent domainEvent)
		{
			switch (domainEvent)
			{
				case HomeUpdatedEvent homeUpdatedEvent:
					var message =
						$"[DispatchDomainEvents] Dispatch HomeUpdatedEvent => Updated home {homeUpdatedEvent.Name} ";
					_logger.Information(message);
					break;
				default:
					throw new SmartHubException("Unknown event error");
			}
		}

	}
}
