using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.UseCases.SignalR;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	/// <inheritdoc cref="IEventDispatcher"/>
	public class EventDispatcher : IEventDispatcher
	{
		private readonly IChannelManager _channelManager;
		private IDisposable _disposable;
		private readonly IHubContext<EventHub, IServerHub> _eventHubContext;
		private readonly ILogger _logger = Log.ForContext(typeof(EventDispatcher));
		public EventDispatcher(IChannelManager channelManager, IHubContext<EventHub, IServerHub> eventHubContext)
		{
			_channelManager = channelManager;
			_eventHubContext = eventHubContext;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_disposable = _channelManager
				.GetChannel(ChannelNames.System)
				.Subscribe(Dispatch);
			_logger.Information("Start EventDispatcher in background.");
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_disposable.Dispose();
			_logger.Information("Stop EventDispatcher.");
			return Task.CompletedTask;
		}

		private void Dispatch(IBaseEvent baseEvent)
		{
			_eventHubContext.Clients.All.SendEvent(baseEvent);
			var outputString = BuildOutputString(baseEvent);
			_logger.Information(outputString);
			// hier dann alle aus den events EventEntities bauen und in die db speichern
		}

		private static string BuildOutputString(IBaseEvent baseEvent)
		{
			var propsDictionary = baseEvent.GetType().GetProperties()
					.Where(x => x.GetValue(baseEvent) != null)
					.ToDictionary(x => x.Name, x => x.GetValue(baseEvent));
			var propsString = string.Join(", ", propsDictionary.Select(x => x.Key + ": " + x.Value));
			return $"Dispatch {baseEvent.GetType().Name} - {propsString} ";
		}
	}
}
