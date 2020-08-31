using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.SignalR;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Infrastructure.Shared.Services.Dispatchers
{
	/// <inheritdoc cref="IEventDispatcher"/>
	public class EventDispatcher : IEventDispatcher
	{
		private readonly IChannelManager _channelManager;
		private IDisposable _disposable;
		private readonly IHubContext<EventHub, IServerHub> _hubContext;
		private readonly ILogger _log = Log.ForContext(typeof(EventDispatcher));
		public EventDispatcher(IChannelManager channelManager, IHubContext<EventHub, IServerHub> hubContext)
		{
			_channelManager = channelManager;
			_hubContext = hubContext;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_disposable = _channelManager
				.GetChannel(EventTypes.All)
				.Subscribe(Dispatch);
			_log.Information("Start EventDispatcher in background.");
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_disposable.Dispose();
			_log.Information("Stop EventDispatcher.");
			return Task.CompletedTask;
		}

		private void Dispatch(IEvent baseEvent)
		{
			if (baseEvent == null)
			{
				return;
			}
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
			_log.Information("Dispatch Event => {@Name} - {EventType}",
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
					var message = $"Dispatch LoginEvent =>  UserName: {loginEvent.UserName}; Login state: {loginEvent.Successful} ";
					_log.Information(message);
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
						$"Dispatch HomeUpdatedEvent => Updated home {homeUpdatedEvent.Name} ";
					_log.Information(message);
					break;
				default:
					throw new SmartHubException("Unknown event error");
			}
		}

	}
}
