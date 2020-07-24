using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.SignalR;

namespace SmartHub.Api.LogSink
{
    public class SignalrSink<THub, T> : ILogEventSink where THub : Hub<T> where T : class, IServerHub
    {
        private readonly IFormatProvider _formatProvider;
        private readonly IServiceProvider _serviceProvider;
        private IHubContext<THub, T> _hubContext;

        public SignalrSink(IFormatProvider formatProvider, IServiceProvider serviceProvider = null)
        {
            _formatProvider = formatProvider;
            _serviceProvider = serviceProvider;
        }

        public void Emit(LogEvent logEvent)
        {

             _hubContext = _serviceProvider.GetRequiredService<IHubContext<THub, T>>();
            var serverLog = new ServerLog(logEvent.Timestamp.ToString("dd.mm.yyyy HH:mm:ss.fff"),
                logEvent.Level.ToString(),
                logEvent.RenderMessage(_formatProvider),
                logEvent.Exception?.ToString() ?? "-");
            _hubContext.Clients.All.SendLog(serverLog);
        }
    }
}