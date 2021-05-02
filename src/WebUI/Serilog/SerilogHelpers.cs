using System;
using Serilog;
using Serilog.Extensions.Hosting;
using Microsoft.Extensions.Hosting;

namespace SmartHub.WebUI.Serilog
{
    internal static class SerilogHelpers
    {
        /// <summary>
        /// Creates a logger used during application initialisation.
        /// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
        /// </summary>
        /// <returns>A logger that can load a new configuration.</returns>
        internal static ReloadableLogger CreateBootstrapLogger() =>
            new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateBootstrapLogger();

        /// <summary>
        /// Configures a logger used during the applications lifetime.
        /// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
        /// </summary>
        internal static void ConfigureReloadableLogger(
            HostBuilderContext context,
            IServiceProvider services,
            LoggerConfiguration configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .WriteTo.Conditional(
                    x => context.HostingEnvironment.IsDevelopment(),
                    x => x.Console().WriteTo.Debug());
        // loggerConfig
        //     .ReadFrom.Configuration(context.Configuration)
        //     .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        //     .Enrich.With(new LogFilePathEnricher(service))
        //     .WriteTo.Map(LogFilePathEnricher.LogFilePathPropertyName,
        //         (logFilePath, configuration) =>
        //         {
        //             if (context.Configuration.GetValue<bool>("LogToFile") ||
        //                 context.HostingEnvironment.IsProduction())
        //             {
        //                 configuration.File($"{logFilePath}");
        //             }
        //         }, 1)
        //     ;
    }
}
