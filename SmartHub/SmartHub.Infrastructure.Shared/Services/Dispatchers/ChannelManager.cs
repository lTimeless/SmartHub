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
		private Dictionary<ChannelNames, Subject<IEvent>> ChannelMessageDictionary { get; set; }

		public Task StartAsync(CancellationToken cancellationToken)
		{
			ChannelMessageDictionary = new Dictionary<ChannelNames, Subject<IEvent>>();
			// Creates channels for all EventTypes
			foreach (var type in (ChannelNames[])Enum.GetValues(typeof(ChannelNames)))
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

		public Task RemoveChannel(ChannelNames channelName)
		{
			if (ChannelMessageDictionary.ContainsKey(channelName))
			{
				var res = ChannelMessageDictionary.Remove(channelName);
				_log.Information(res ? $"Remove channel with name {channelName}" : $"Error removing channel with name {channelName}");
				return Task.CompletedTask;
			}

			_log.Information($"Could not find channel with name {channelName}");
			return Task.CompletedTask;
		}

		public Task Clear()
		{
			ChannelMessageDictionary.Clear();
			return Task.CompletedTask;
		}

		public IObservable<IEvent> GetChannel(ChannelNames channelName)
		{
			_log.Information($"Get channel with name {channelName}");
			return ChannelMessageDictionary[channelName];
		}

		public Task PublishNextToChannel(ChannelNames channelName, IEvent message)
		{
			if (message is null)
			{
				return Task.CompletedTask;
			}

			var channel = ChannelMessageDictionary[channelName];
			channel.OnNext(message);
			return Task.CompletedTask;
		}

		public Task PublishErrorToChannel(ChannelNames channelName, Exception exception)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(channelName);
			channel.OnError(exception);
			_log.Information($"Publish Error to channel => {channelName}");
			return Task.CompletedTask;
		}

		public Task PublishCompleteToChannel(ChannelNames channelName)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(channelName);
			channel.OnCompleted();
			return Task.CompletedTask;
		}

		private void AddChannel(ChannelNames channelName)
		{
			if (ChannelMessageDictionary.ContainsKey(channelName))
			{
				return;
			}
			var newChannel = new Subject<IEvent>();
			ChannelMessageDictionary.Add(channelName, newChannel);
			_log.Information($"Added new channel => {channelName}");
		}
	}
}
