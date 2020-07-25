using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Application.Common.Interfaces
{
	public interface IChannelManager : IInitialize, IHostedService, IDisposable
	{
		/// <summary>
		/// Gets the given Channel observable.
		/// if channel does not exists it will create a new channel.
		/// </summary>
		/// <param name="eventType">The channel eventType.</param>
		/// <returns></returns>
		public IObservable<IEvent> GetChannel(EventTypes eventType);

		/// <summary>
		/// Publishes an object to the channel.
		/// </summary>
		/// <param name="eventType">The channel eventType.</param>
		/// <param name="message">Any event object.</param>
		public Task PublishNextToChannel(EventTypes eventType, IEvent message);

		/// <summary>
		/// Publishes an error to the channel.
		/// </summary>
		/// <param name="eventType">The channel eventType.</param>
		/// <param name="exception">Any exception that gets throws during the subscription.</param>
		public Task PublishErrorToChannel(EventTypes eventType, Exception exception);

		public Task PublishCompleteToChannel(EventTypes eventType);

		/// <summary>
		/// Removes given channel
		/// </summary>
		/// <param name="eventType">The channel eventType.</param>
		public Task RemoveChannel(EventTypes eventType);

		/// <summary>
		/// Clears all channels
		/// </summary>
		public Task Clear();
	}
}
