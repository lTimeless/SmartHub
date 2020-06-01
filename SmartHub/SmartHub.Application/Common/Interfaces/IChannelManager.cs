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
		public IObservable<object> GetChannel(ChannelEventEnum channelName);

		/// <summary>
		/// Creates a new channel with given name
		/// </summary>
		/// <param name="channelName"></param>
		public Task AddChannel(ChannelEventEnum channelName);

		/// <summary>
		/// Publish object to the channel
		/// </summary>
		/// <param name="channelName"></param>
		/// <param name="message">any object</param>
		public Task PublishNextToChannel<T>(ChannelEventEnum channelName, T message);

		public Task PublishErrorToChannel(ChannelEventEnum channelName, Exception exception);

		public Task PublishCompleteToChannel(ChannelEventEnum channelName);

		/// <summary>
		/// Removes given channel
		/// </summary>
		/// <param name="channelName"></param>
		public Task RemoveChannel(ChannelEventEnum channelName);

		/// <summary>
		/// Clears all channels
		/// </summary>
		public Task Clear();
	}
}
