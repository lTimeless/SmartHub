using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Sinks.SystemConsole.Themes;
using SmartHub.WebUI.Serilog;
using System;

namespace SmartHub.WebUI.Extensions
{
	internal static class SerilogExtension
	{
		/// <summary>
		///     Creates a logger used during application initialisation.
		///     <see href="https://nblumhardt.com/2020/10/bootstrap-logger/" />.
		/// </summary>
		/// <returns>A logger that can load a new configuration.</returns>
		internal static ReloadableLogger CreateBootstrapLogger()
		{
			return new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.Debug()
				.CreateBootstrapLogger();
		}

		/// <summary>
		///     Configures a logger used during the applications lifetime.
		///     <see href="https://nblumhardt.com/2020/10/bootstrap-logger/" />.
		/// </summary>
		internal static void ConfigureReloadableLogger(HostBuilderContext context, IServiceProvider services,
			LoggerConfiguration configuration)
		{
			configuration
				.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services)
				.Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
				.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
				// With SourceContext in production
				.WriteTo.Async(x => x.Conditional(_ => context.HostingEnvironment.IsProduction(),
					c => c.Console(theme: SystemConsoleTheme.Literate,
						outputTemplate:
						"[{Timestamp:HH:mm:ss.fff} - {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")))
				// No SourceContext  in development
				.WriteTo.Async(x => x.Conditional(_ => !context.HostingEnvironment.IsProduction(),
					c => c.Console(theme: SystemConsoleTheme.Literate,
						outputTemplate: "[{Timestamp:HH:mm:ss.fff} - {Level:u3}] {Message:lj}{NewLine}{Exception}")))
				// TODO look how to write to file via conditional and async code,only when value"LogToFile" is set -> how to get the actual filepath that is set in the DirectoryCreation
				// .WriteTo.Conditional(
				// 	x => context.HostingEnvironment.IsProduction(),
				// 	x => x.Async(c => c.File()))
				.WriteTo.Map(LogFilePathEnricher.LogFilePathPropertyName,
					(logFilePath, conf) =>
					{
						if (context.Configuration.GetValue<bool>("LogToFile") ||
						    context.HostingEnvironment.IsProduction())
						{
							conf.File($"{logFilePath}");
						}
					}, 1);
		}
	}
}