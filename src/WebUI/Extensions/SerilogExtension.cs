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
		/// </summary>
		/// <returns>A logger that can load a new configuration.</returns>
		internal static ReloadableLogger CreateBootstrapLogger() =>
			new LoggerConfiguration()
				.WriteTo.Console()
				.CreateBootstrapLogger();

		/// <summary>
		///     Configures a logger used during the applications lifetime.
		/// </summary>
		internal static void ConfigureReloadableLogger(HostBuilderContext context, IServiceProvider services,
			LoggerConfiguration configuration) =>
			configuration
				.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services)
				.Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
				.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
				.Enrich.With(new LogFilePathEnricher(services))
				// Production
				.WriteTo.Async(x => x.Conditional(_ => context.HostingEnvironment.IsProduction(),
					c => c.Console(theme: SystemConsoleTheme.Literate,
						outputTemplate:
						"[{Timestamp:HH:mm:ss.fff} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")))
				.WriteTo.Conditional(
					_ => context.HostingEnvironment.IsProduction(),
					x => x.Async(c => c.Map(LogFilePathEnricher.LogFilePathPropertyName,
						(logFilePath, conf) =>
						{
							if (context.Configuration.GetValue<bool>("LogToFile") ||
							    context.HostingEnvironment.IsProduction())
							{
								conf.File($"{logFilePath}");
							}
						}, 1)))
				// Development
				.WriteTo.Async(x => x.Conditional(_ => !context.HostingEnvironment.IsProduction(),
					c => c.Console(theme: SystemConsoleTheme.Literate,
						outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")));

		// TODO write to SerilogExpressionTemplate when it works properly, now it shows some weird symbols
		// 	// Production
		// 	.WriteTo.Async(x => x.Conditional(_ => context.HostingEnvironment.IsProduction(),
		// 		c => c.Console(new ExpressionTemplate(
		// 			"[{@t:HH:mm:ss.fff} {@l:u3} {Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}] {@m:lj}\n{@x}",
		// 			theme: TemplateTheme.Literate))))
		// 	// Development
		// 	.WriteTo.Async(x => x.Conditional(_ => !context.HostingEnvironment.IsProduction(),
		// 		c => c.Console(new ExpressionTemplate(
		// 			"[{@t:HH:mm:ss.fff} {@l:u3} {Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}] {@m:lj}\n{@x}",
		// 			theme: TemplateTheme.Code))))
		// ;
	}
}