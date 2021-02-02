using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using SmartHub.WebUI.Extensions;
using SmartHub.WebUI.Serilog;
using System;
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
						.WriteTo.Elasticsearch(
							new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
							{
								AutoRegisterTemplate = true,
								NumberOfShards = 2,
								NumberOfReplicas = 1,
								IndexFormat =
									$"{context.Configuration["SmartHub:ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
							});
				})
				.ConfigureLogging((_, config) => config.ClearProviders())
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseKestrel();
					webBuilder.UseStartup<Startup>();
				});
	}
}
