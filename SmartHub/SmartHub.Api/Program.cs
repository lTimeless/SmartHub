using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.AspNetCore.SignalR.Extensions;
using Serilog.Sinks.Elasticsearch;
using SmartHub.Api.Extensions;
using SmartHub.Api.Serilog;
using SmartHub.Application.UseCases.SignalR;

namespace SmartHub.Api
{
	public static class Program
	{
		public static async Task Main(string[] args) =>
			await CreateHostBuilder(args)
				.Build()
				.MigrateDatabase(false)
				.RunAsync()
				.ConfigureAwait(false);

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
					configurationBuilder.AddEnvironmentVariables()
						.AddCommandLine(args);
				})
				.UseSerilog((context, service, loggerConfig) =>
				{
					loggerConfig
						.ReadFrom.Configuration(context.Configuration)
						.Enrich.WithProperty("Environment",context.HostingEnvironment.EnvironmentName)
						.Enrich.With(new LogFilePathEnricher(service))
						.WriteTo.Map(LogFilePathEnricher.LogFilePathPropertyName,
							(logFilePath, configuration) =>
							{
								if (context.Configuration.GetValue<bool>("LogToFile") || context.HostingEnvironment.IsProduction())
								{
									configuration.File($"{logFilePath}");
								}
							},1)
						.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
						{
							AutoRegisterTemplate = true,
							NumberOfShards = 2,
							NumberOfReplicas = 1,
							IndexFormat = $"{context.Configuration["SmartHub:ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".","-")}-{DateTime.UtcNow:yyyy-MM}"
						})
						.WriteTo.SignalRSink<LogHub, IServerHub>(
							LogEventLevel.Information,
							service,
							null,
							new string[] {},
							new string[] {},
							new string[] {}
							);
				})
				.ConfigureLogging((_, config) => config.ClearProviders())
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseKestrel();
					webBuilder.UseStartup<Startup>();
				});
	}
}
