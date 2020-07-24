using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using SmartHub.Api.LogSink;

namespace SmartHub.Api.Extensions
{
    public static class SignalrSinkExtension
    {
        public static LoggerConfiguration SignalrSink(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel logEventLevel,
            IServiceProvider serviceProvider = null,
            IFormatProvider formatProvider = null
            )
        {
            return loggerConfiguration.Sink(new SignalrSink(formatProvider, serviceProvider),logEventLevel);
        }
    }
}