using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	public class ChannelManager : IChannelManager
	{
		private Dictionary<ChannelEvent, Subject<object>> ChannelMessageDictionary { get; }

		public ChannelManager()
		{
			ChannelMessageDictionary = new Dictionary<ChannelEvent, Subject<object>>();
		}

		public Task RemoveChannel(ChannelEvent channelName)
		{
			if (ChannelMessageDictionary.ContainsKey(channelName))
			{
				var res = ChannelMessageDictionary.Remove(channelName);
				Log.Information(res ? $"[{nameof(RemoveChannel)}] Remove channel with name {channelName}" : $"[{nameof(RemoveChannel)}] Error removing channel with name {channelName}");
				return Task.CompletedTask;
			}
			else
			{
				Log.Information($"[{nameof(RemoveChannel)}] Could not find channel with name {channelName}");
				return Task.CompletedTask;
			}
		}

		public Task Clear()
		{
			ChannelMessageDictionary.Clear();
			return Task.CompletedTask;
		}

		public IObservable<object> GetChannel(ChannelEvent channelName)
		{
			if (ChannelMessageDictionary.ContainsKey(channelName))
			{
				Log.Information($"[{nameof(GetChannel)}] Get channel with name {channelName}");
				return ChannelMessageDictionary[channelName];
			}
			Log.Information($"[{nameof(GetChannel)}] Channel with name {channelName} was not found... creating a new channel...");
			AddChannel(channelName);
			return ChannelMessageDictionary[channelName];
		}

		public Task AddChannel(ChannelEvent channelName)
		{
			if (ChannelMessageDictionary.ContainsKey(channelName))
			{
				return Task.CompletedTask;
			}

			var newChannel = new Subject<object>();
			ChannelMessageDictionary.Add(channelName, newChannel);
			Log.Information($"[{nameof(AddChannel)}] Added new channel => {channelName}");
			return Task.CompletedTask;
		}

		public Task PublishNextToChannel<T>(ChannelEvent channelName, T message)
		{
			if (message is null)
			{
				return Task.CompletedTask;
			}
			var channel = ChannelMessageDictionary.GetValueOrDefault(channelName);
			if (channel is null)
			{
				throw new SmartHubException($"[{nameof(PublishNextToChannel)}] Channel {channelName} is null ");
			}
			channel.OnNext(message);
			return Task.CompletedTask;
		}

		public Task PublishErrorToChannel(ChannelEvent channelName, Exception exception)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(channelName);
			channel.OnError(exception);
			Log.Information($"[{nameof(PublishErrorToChannel)}] Publish Error to channel => {channelName}");
			return Task.CompletedTask;
		}

		public Task PublishCompleteToChannel(ChannelEvent channelName)
		{
			var channel = ChannelMessageDictionary.GetValueOrDefault(channelName);
			channel.OnCompleted();
			return Task.CompletedTask;
		}
	}
}
