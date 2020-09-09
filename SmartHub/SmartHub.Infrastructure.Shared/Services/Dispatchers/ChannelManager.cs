using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Infrastructure.Shared.Services.Dispatchers
{
	/// <inheritdoc cref="IChannelManager"/>
	public class ChannelManager : IChannelManager
	{
		private readonly ILogger _log = Log.ForContext<ChannelManager>();
		private Dictionary<EventTypes, Subject<IEvent>> ChannelMessageDictionary { get; set; }

		public Task StartAsync(CancellationToken cancellationToken)
		{
			ChannelMessageDictionary = new Dictionary<EventTypes, Subject<IEvent>>();
			// Creates channels for all EventTypes
			foreach (var type in (EventTypes[])Enum.GetValues(typeof(EventTypes)))
			{
				AddChannel(type);
			}
			_log.Information("Start ChannelManager in background.");
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_log.Information("Stop ChannelManager.");
			return Task.CompletedTask;
		}

		public Task RemoveChannel(EventTypes eventType)
		{
			if (ChannelMessageDictionary.ContainsKey(eventType))
			{
				var res = ChannelMessageDictionary.Remove(eventType);
				_log.Information(res ? $"Remove channel with name {eventType}" : $"Error removing channel with name {eventType}");
				return Task.CompletedTask;
			}

			_log.Information($"Could not find channel with name {eventType}");
			return Task.CompletedTask;
		}

		public Task Clear()
		{
			ChannelMessageDictionary.Clear();
			return Task.CompletedTask;
		}

		public IObservable<IEvent> GetChannel(EventTypes eventType)
		{
			_log.Information($"Get channel with name {eventType}");
			return ChannelMessageDictionary[eventType];
		}

		public Task PublishNextToChannel(EventTypes eventType, IEvent message)
		{
			if (message is null)
			{
				return Task.CompletedTask;
			}

			var channel = ChannelMessageDictionary[eventType];
			channel.OnNext(message);

			var allEventsChannel = ChannelMessageDictionary[EventTypes.All];
			allEventsChannel.OnNext(message);
			return Task.CompletedTask;
		}

		public Task PublishErrorToChannel(EventTypes eventType, Exception exception)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(eventType);
			channel.OnError(exception);
			_log.Information($"Publish Error to channel => {eventType}");
			return Task.CompletedTask;
		}

		public Task PublishCompleteToChannel(EventTypes eventType)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(eventType);
			channel.OnCompleted();
			return Task.CompletedTask;
		}

		private void AddChannel(EventTypes eventType)
		{
			if (ChannelMessageDictionary.ContainsKey(eventType))
			{
				return;
			}
			var newChannel = new Subject<IEvent>();
			ChannelMessageDictionary.Add(eventType, newChannel);
			_log.Information($"Added new channel => {eventType}");
		}
	}
}
