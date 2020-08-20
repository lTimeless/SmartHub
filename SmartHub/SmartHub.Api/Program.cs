using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.AspNetCore.SignalR.Extensions;
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
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.UseSerilog((hostingContext, service, loggerConfig) =>
				{
					loggerConfig
						.ReadFrom.Configuration(hostingContext.Configuration)
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
