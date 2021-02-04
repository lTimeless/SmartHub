using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using SmartHub.Application.Common.Helpers;
using SmartHub.WebUI.Extensions;
using SmartHub.WebUI.Serilog;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartHub.WebUI
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateBootstrapLogger();

			try
			{
				await CreateHostBuilder(args)
					.Build()
					.MigrateDatabase()
					.RunAsync()
					.ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
				{
					var env = hostingContext.HostingEnvironment;
					configurationBuilder
						.AddJsonFile("appsettings.json", true, true)
						.AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true);

					if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
					{
						var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
						if (appAssembly != null)
						{
							configurationBuilder.AddUserSecrets(appAssembly, true);
						}
					}
					configurationBuilder
						.AddCommandLine(args)
						.AddEnvironmentVariables();
				})
				.UseSerilog((context, service, loggerConfig) =>
				{
					loggerConfig
						.ReadFrom.Configuration(context.Configuration)
						.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
						.Enrich.With(new LogFilePathEnricher(service))
						.WriteTo.Map(LogFilePathEnricher.LogFilePathPropertyName,
							(logFilePath, configuration) =>
							{
								if (context.Configuration.GetValue<bool>("LogToFile") ||
								    context.HostingEnvironment.IsProduction())
								{
									configuration.File($"{logFilePath}");
								}
							}, 1)
;
				})
				.ConfigureLogging((_, config) => config.ClearProviders())
				.ConfigureWebHostDefaults(webBuilder =>
				{
					var host = IpAddressUtils.ShowLocalIpv4();
					webBuilder.UseKestrel(opt =>
					{
						opt.ListenLocalhost(5001, conf => conf.UseHttps());
						opt.Listen(host, 5000);
						opt.Listen(host, 5001, conf => conf.UseHttps());

					});
					webBuilder.UseStartup<Startup>();
				});
	}
}
