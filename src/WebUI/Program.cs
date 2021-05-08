using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SmartHub.WebUI.Extensions;
using SmartHub.WebUI.Serilog;
using System;
using System.Threading.Tasks;

namespace SmartHub.WebUI
{
	public static class Program
	{
		public static async Task<int> Main(string[] args)
		{
			Log.Logger = SerilogExtension.CreateBootstrapLogger();
			try
			{
				var host = CreateHostBuilder(args).Build();
				var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
				hostEnvironment.ApplicationName = AssemblyInformation.Current.Product;

				// TODO add in initService
				// Log.Information("Started {Application} in {Environment} mode", hostEnvironment.ApplicationName, hostEnvironment.EnvironmentName);

				await host.MigrateDatabase().RunAsync();
				// Log.Information("Stopped {Application} in {Environment} mode", hostEnvironment.ApplicationName, hostEnvironment.EnvironmentName);
				return 0;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder()
				// .ConfigureAppConfiguration((hostingContext, config) =>
				// 	HostExtension.AddConfiguration(config, hostingContext.HostingEnvironment, args))
				.UseSerilog(SerilogExtension.ConfigureReloadableLogger)
				.UseDefaultServiceProvider(
					(context, options) =>
					{
						var isDevelopment = context.HostingEnvironment.IsDevelopment();
						options.ValidateScopes = isDevelopment;
						options.ValidateOnBuild = isDevelopment;
					})
				.ConfigureLogging((_, config) => config.ClearProviders())
				.ConfigureWebHost(HostExtension.ConfigureWebHostBuilder)
				.UseConsoleLifetime();
		}
	}
}