using System;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using SmartHub.Api.LogSink;
using SmartHub.Application.UseCases.SignalR;

namespace SmartHub.Api.Extensions
{
    public static class SignalrSinkExtension
    {
        public static LoggerConfiguration SignalrSink<THub, T>(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel logEventLevel,
            IServiceProvider serviceProvider = null,
            IFormatProvider formatProvider = null
            ) where THub : Hub<T> where T : class, IServerHub
        {
            return loggerConfiguration.Sink(new SignalrSink<THub, T>(formatProvider, serviceProvider),logEventLevel);
        }
    }
}