using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IChannelManager : IHostedService
	{
		/// <summary>
		/// Gets the given Channel observable.
		/// if channel does not exists it will create a new channel.
		/// </summary>
		/// <param name="channelName">The channel eventType.</param>
		/// <returns></returns>
		public IObservable<IBaseEvent> GetChannel(ChannelNames channelName);

		/// <summary>
		/// Publishes an object to the channel.
		/// </summary>
		/// <param name="channelName">The channel name</param>
		/// <param name="message">Any event object.</param>
		public Task PublishNextToChannel(ChannelNames channelName, IBaseEvent message);

		/// <summary>
		/// Publishes an error to the channel.
		/// </summary>
		/// <param name="channelName">The channel name</param>
		/// <param name="exception">Any exception that gets throws during the subscription.</param>
		public Task PublishErrorToChannel(ChannelNames channelName, Exception exception);

		public Task PublishCompleteToChannel(ChannelNames channelName);

		/// <summary>
		/// Removes given channel
		/// </summary>
		/// <param name="channelName">The channel name</param>
		public Task RemoveChannel(ChannelNames channelName);

		/// <summary>
		/// Clears all channels
		/// </summary>
		public Task Clear();
	}
}
