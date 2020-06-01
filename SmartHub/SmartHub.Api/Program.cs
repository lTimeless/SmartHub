using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SmartHub.Api.Extensions;

namespace SmartHub.Api
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var host = await CreateHostBuilder(args)
				.Build()
				.AsciiLogo()
				.WelcomeText()
				.MigrateDatabase(false);

			await host.RunAsync().ConfigureAwait(false);
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseKestrel();
					webBuilder.UseStartup<Startup>();
					webBuilder.UseSerilog((hostingContext, loggerConfig) =>
					{
						loggerConfig
							.ReadFrom.Configuration(hostingContext.Configuration);
					});
				})
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureLogging((_, config) => config.ClearProviders());
	}
}
