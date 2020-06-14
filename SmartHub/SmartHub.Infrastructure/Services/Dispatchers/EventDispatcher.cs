using SmartHub.Domain.Common.EventTypes;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
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
		private static readonly string NewLine = Environment.NewLine;

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
				case HomeUpdatedEvent homeUpdatedEvent:
					_logger.Information($"[{nameof(DispatchDomainEvents)}] Dispatch {nameof(HomeUpdatedEvent)} =>  " +
					                    homeUpdatedEvent.Name == null ? "" : $"Updated name to: {homeUpdatedEvent.Name} {NewLine}" +
					                    homeUpdatedEvent.Description is null ? "" : $"Updated description to: {homeUpdatedEvent.Description} {NewLine}" +
					                    homeUpdatedEvent.NewUser is null ? "" : $"Added new user: {homeUpdatedEvent.NewUser!.UserName}{NewLine}" +
					                    homeUpdatedEvent.NewGroup is null ? "" : $"Added new group: {homeUpdatedEvent.NewGroup!.Name}{NewLine}" +
					                    homeUpdatedEvent.NewDevice is null ? "" : $"Added new device: {homeUpdatedEvent.NewDevice!.Name}{NewLine}" +
					                    homeUpdatedEvent.NewPlugin is null ? "" : $"Added new plugin: {homeUpdatedEvent.NewPlugin!.Name}{NewLine}" +
					                    homeUpdatedEvent.NewSetting is null ? "" : $"Added new setting: {homeUpdatedEvent.NewSetting!.Name}{NewLine}" +
					                    homeUpdatedEvent.NewAddress is null ? "" : $"Updated address to: {ValueObject.Print}{NewLine}");
                    // hier an den jeweiligen service schicken der das event verarbeiten soll, z.B email, notification, signalr
					break;

				default:
					throw new SmartHubException("Unknown event error");
			}
		}
	}
}
