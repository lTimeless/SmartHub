using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	/// <inheritdoc cref="IEventDispatcher"/>
	public class EventDispatcher : IHostedService
	{
		private readonly IChannelManager _channelManager;
		private IDisposable? _disposable;
		private readonly ILogger _logger = Log.ForContext(typeof(EventDispatcher));
		public EventDispatcher(IChannelManager channelManager)
		{
			_channelManager = channelManager;
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
			_disposable?.Dispose();
			_logger.Information("Stop EventDispatcher.");
			return Task.CompletedTask;
		}

		private void Dispatch(IBaseEvent baseEvent)
		{
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
