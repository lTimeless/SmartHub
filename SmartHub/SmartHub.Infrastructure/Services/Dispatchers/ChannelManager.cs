using Serilog;
using SmartHub.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	public class ChannelManager : IChannelManager
	{
		private Dictionary<EventTypes, Subject<IEvent>> ChannelMessageDictionary { get; set; }

		public ChannelManager()
		{
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			Log.Information("[ChannelManager] ChannelManager started in background");
			await Init();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			Log.Information("[ChannelManager] ChannelManager stopped");
			Dispose();
			return Task.CompletedTask;
		}

		private Task Init()
		{
			ChannelMessageDictionary = new Dictionary<EventTypes, Subject<IEvent>>();

			// Creates channels for all EventTypes
			foreach (var type in (EventTypes[])Enum.GetValues(typeof(EventTypes)))
			{
				AddChannel(type);
			}

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			Clear();
		}

		public Task RemoveChannel(EventTypes eventType)
		{
			if (ChannelMessageDictionary.ContainsKey(eventType))
			{
				var res = ChannelMessageDictionary.Remove(eventType);
				Log.Information(res ? $"[{nameof(RemoveChannel)}] Remove channel with name {eventType}" : $"[{nameof(RemoveChannel)}] Error removing channel with name {eventType}");
				return Task.CompletedTask;
			}

			Log.Information($"[{nameof(RemoveChannel)}] Could not find channel with name {eventType}");
			return Task.CompletedTask;
		}

		public Task Clear()
		{
			ChannelMessageDictionary.Clear();
			return Task.CompletedTask;
		}

		public IObservable<IEvent> GetChannel(EventTypes eventType)
		{
			Log.Information($"[{nameof(GetChannel)}] Get channel with name {eventType}");
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
			Log.Information($"[{nameof(PublishErrorToChannel)}] Publish Error to channel => {eventType}");
			return Task.CompletedTask;
		}

		public Task PublishCompleteToChannel(EventTypes eventType)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(eventType);
			channel.OnCompleted();
			return Task.CompletedTask;
		}

		private Task AddChannel(EventTypes eventType)
		{
			if (ChannelMessageDictionary.ContainsKey(eventType))
			{
				return Task.CompletedTask;
			}

			var newChannel = new Subject<IEvent>();
			ChannelMessageDictionary.Add(eventType, newChannel);
			Log.Information($"[{nameof(AddChannel)}] Added new channel => {eventType}");
			return Task.CompletedTask;
		}
	}
}
