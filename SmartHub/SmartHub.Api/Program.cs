using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
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
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseKestrel();
					webBuilder.UseStartup<Startup>();
				})

				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.UseSerilog((hostingContext, service, loggerConfig) =>
				{
					loggerConfig
						.ReadFrom.Configuration(hostingContext.Configuration)
						.Enrich.FromLogContext()
						.WriteTo.SignalrSink<LogHub, IServerHub>(LogEventLevel.Debug, service );
				})
				.ConfigureLogging((_, config) => config.ClearProviders());
	}
}
