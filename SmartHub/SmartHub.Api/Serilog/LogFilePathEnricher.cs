using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog.Core;
using Serilog.Events;
using SmartHub.Domain.Common;

namespace SmartHub.Api.Serilog
{
    public class LogFilePathEnricher: ILogEventEnricher
    {
        private readonly IOptionsMonitor<ApplicationConfig> _options;
        private string? _cachedLogFilePath;
        private LogEventProperty? _cachedLogFilePathProperty;

        public const string LogFilePathPropertyName = "LogFilePath";

        public LogFilePathEnricher(IServiceProvider serviceProvider)
        {
            _options = serviceProvider.GetRequiredService<IOptionsMonitor<ApplicationConfig>>();
        }
        /// <summary>
        /// Adds a property to each log statement and the logFilepath
        /// </summary>
        /// <param name="logEvent">The current LogEvent</param>
        /// <param name="propertyFactory">The current propertyFactory for each log statement</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
             var logFilePath = _options.CurrentValue.LogFolderPath + $"\\log-{DateTime.Now:yyyyMMdd}.txt";// Read path from your appsettings.json
                // Check for null, etc...

            LogEventProperty logFilePathProperty;

            if (logFilePath.Equals(_cachedLogFilePath))
            {
                // Path hasn't changed, so let's use the cached property
                logFilePathProperty = _cachedLogFilePathProperty!;
            }
            else
            {
                // We've got a new path for the log. Let's create a new property
                // and cache it for future log events to use
                _cachedLogFilePath = logFilePath;

                _cachedLogFilePathProperty = logFilePathProperty =
                    propertyFactory.CreateProperty(LogFilePathPropertyName, logFilePath);
            }

            logEvent.AddPropertyIfAbsent(logFilePathProperty);
        }
    }
}