using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	/// <inheritdoc cref="IChannelManager"/>
	public class ChannelManager : IChannelManager
	{
		private readonly ILogger _log = Log.ForContext<ChannelManager>();
		private Dictionary<ChannelNames, Subject<IBaseEvent>> ChannelMessageDictionary { get; set; } = new Dictionary<ChannelNames, Subject<IBaseEvent>>();

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_log.Information("Start ChannelManager in background.");

			// Creates channels for all ChannelNames
			foreach (var type in (ChannelNames[])Enum.GetValues(typeof(ChannelNames)))
			{
				AddChannel(type);
			}
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

		public IObservable<IBaseEvent> GetChannel(ChannelNames channelName)
		{
			return ChannelMessageDictionary[channelName];
		}

		public Task PublishNextToChannel(ChannelNames channelName, IBaseEvent? message)
		{
			if (message is null)
			{
				return Task.CompletedTask;
			}

			var channel = ChannelMessageDictionary[channelName];
			channel?.OnNext(message);
			return Task.CompletedTask;
		}

		public Task PublishErrorToChannel(ChannelNames channelName, Exception exception)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(channelName);
			channel?.OnError(exception);
			_log.Information($"Error on channel => {channelName}");
			return Task.CompletedTask;
		}

		public Task PublishCompleteToChannel(ChannelNames channelName)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(channelName);
			channel?.OnCompleted();
			return Task.CompletedTask;
		}

		private void AddChannel(ChannelNames channelName)
		{
			if (ChannelMessageDictionary.ContainsKey(channelName))
			{
				return;
			}
			var newChannel = new Subject<IBaseEvent>();
			ChannelMessageDictionary.Add(channelName, newChannel);
			_log.Information($"Added new channel => {channelName}");
		}
	}
}
