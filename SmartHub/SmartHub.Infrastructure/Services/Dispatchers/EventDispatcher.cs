using SmartHub.Domain.Common.EventTypes;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using Serilog;
using SmartHub.Domain.Enums;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.Common.Exceptions;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	public class EventDispatcher : IEventDispatcher
	{
		private readonly IChannelManager _channelManager;
		private readonly ILogger _logger;

		public EventDispatcher(IChannelManager channelManager, ILogger logger)
		{
			_channelManager = channelManager;
			_logger = logger;
			if (_channelManager is null)
			{
				_channelManager = new ChannelManager();
			}
			_ = _channelManager
				.GetChannel(ChannelEventEnum.Events)
				.Subscribe(Dispatch);
		}

		private void Dispatch(object baseEvent)
		{
			if (baseEvent is null)
			{
				return;
			}
			else if (baseEvent is IApplicationEvent appEvent)
			{
				DispatchApplicationEvent(appEvent);
				return;
			}
			else if (baseEvent is IEnumerable<IDomainEvent> events)
			{
				foreach (var item in events)
				{
					DispatchDomainEvents(item);
				}
				return;
			}
			else
			{
				Dispatch(baseEvent);
				return;
			}
		}

		private void Dispatch(IEvent baseEvent)
		{
			_logger.Information($"[{nameof(Dispatch)}] Dispatch {nameof(IEvent)}=> {baseEvent.GetType().Name}");
			// hier dann alle aus den events Evententitys bauen und in die db speichern
			// und an signalR geben für liveEvents
		}

		private void DispatchApplicationEvent(IApplicationEvent applicataionEvent)
		{
			switch (applicataionEvent)
			{
				case LoginEvent loginEvent:
					_logger.Information($"[{nameof(DispatchApplicationEvent)}] Dispatch {nameof(LoginEvent)} =>  " +
						$"UserName: {loginEvent.UserName}; " +
						$"Login state: {loginEvent.Successful}; "
						); // hier an den jeweiligen service schicken der das event verarbeiten soll, z.B email, notification signalr
					break;

				default:
					throw new SmartHubException("Unknown event error");
			}
		}

		private void DispatchDomainEvents(IDomainEvent domainEvent)
		{
			switch (domainEvent)
			{
				case HomeChangedEvent homeChangedEvent:
					_logger.Information($"[{nameof(DispatchDomainEvents)}] Dispatch {nameof(HomeChangedEvent)} =>  " +
						$"id: {homeChangedEvent.HomeId}; " +
						$"name: {homeChangedEvent.Name ?? "unchanged"}; " +
						$"description: {homeChangedEvent.Description ?? "unchanged"}; " +
						$"users: {(homeChangedEvent.Users.IsNullOrEmpty() ? "unchanged" : string.Join(",", homeChangedEvent.Users))}; " +
						$"groups: {(homeChangedEvent.Groups.IsNullOrEmpty() ? "unchanged" : string.Join(",", homeChangedEvent.Groups))}; " +
						$"devices: {(homeChangedEvent.Devices.IsNullOrEmpty() ? "unchanged" : string.Join(",", homeChangedEvent.Devices))}; " +
						$"settings: {(homeChangedEvent.Settings.IsNullOrEmpty() ? "unchanged" : string.Join(",", homeChangedEvent.Settings))}; " +
						$"address: {(homeChangedEvent.Address is null ? "unchanged" : ValueObject.Print)}; "
						); // hier an den jeweiligen service schicken der das event verarbeiten soll, z.B email, notification signalr
					break;

				case HomeAddPluginEvent addPluginEvent:
					_logger.Information($"[{nameof(DispatchDomainEvents)}] Dispatch {nameof(HomeAddPluginEvent)} =>  " +
						$"Added Plugin: {addPluginEvent.PluginName};");
					break;

				default:
					throw new SmartHubException("Unknown event error");
			}
		}
	}
}
