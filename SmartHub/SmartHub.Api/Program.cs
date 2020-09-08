using System;
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
using SmartHub.Application.UseCases.SignalR;

namespace SmartHub.Api
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args)
				.Build()
				.AsciiLogo()
				.WelcomeText()
				.MigrateDatabase(false);

			await host.RunAsync().ConfigureAwait(false);
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration(b =>
				{
					b.AddCommandLine(args)
						.AddEnvironmentVariables();
				})
				.UseSerilog((context, service, loggerConfig) =>
				{
					loggerConfig
						.ReadFrom.Configuration(context.Configuration)
						.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
						{
							AutoRegisterTemplate = true,
							NumberOfShards = 2,
							NumberOfReplicas = 1,
							IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".","-")}-{DateTime.UtcNow:yyyy-MM}"
						})
						.Enrich.WithProperty("Environment",context.HostingEnvironment.EnvironmentName)
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
