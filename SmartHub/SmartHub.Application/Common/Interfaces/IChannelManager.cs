using SmartHub.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IChannelManager
	{
		/// <summary>
		/// Gets the given Channel observable
		/// if channel does not exists it will create a new channel
		/// </summary>
		/// <param name="channelName"></param>
		/// <returns></returns>
		public IObservable<object> GetChannel(ChannelEvent channelName);

		/// <summary>
		/// Creates a new channel with given name
		/// </summary>
		/// <param name="channelName"></param>
		public Task AddChannel(ChannelEvent channelName);

		/// <summary>
		/// Publish object to the channel
		/// </summary>
		/// <param name="channelName"></param>
		/// <param name="message">any object</param>
		public Task PublishNextToChannel<T>(ChannelEvent channelName, T message);

		public Task PublishErrorToChannel(ChannelEvent channelName, Exception exception);

		public Task PublishCompleteToChannel(ChannelEvent channelName);

		/// <summary>
		/// Removes given channel
		/// </summary>
		/// <param name="channelName"></param>
		public Task RemoveChannel(ChannelEvent channelName);

		/// <summary>
		/// Clears all channels
		/// </summary>
		public Task Clear();
	}
}
