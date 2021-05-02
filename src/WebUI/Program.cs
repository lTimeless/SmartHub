using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.WebUI.Extensions;
using SmartHub.WebUI.Serilog;
using System;
using System.Threading.Tasks;
using System.IO;
using Boxed.AspNetCore;

namespace SmartHub.WebUI
{
	public sealed class Program
	{
		public static async Task<int> Main(string[] args)
		{
			Log.Logger = SerilogHelpers.CreateBootstrapLogger();
			try
			{
				Log.Information("Initialising");
				var host = CreateHostBuilder(args).Build();
				var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
				hostEnvironment.ApplicationName = AssemblyInformation.Current.Product;

				Log.Information(
					"Started {Application} in {Environment} mode",
					hostEnvironment.ApplicationName,
					hostEnvironment.EnvironmentName);

				await host.MigrateDatabase().RunAsync();
				Log.Information(
					"Stopped {Application} in {Environment} mode",
					hostEnvironment.ApplicationName,
					hostEnvironment.EnvironmentName);
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

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.ConfigureHostConfiguration(
					configurationBuilder => configurationBuilder
						.AddEnvironmentVariables(prefix: "DOTNET_")
						.AddIf(args is not null, x => x.AddCommandLine(args)))
				.ConfigureAppConfiguration((hostingContext, config) =>
					HostExtension.AddConfiguration(config, hostingContext.HostingEnvironment, args))
				.UseSerilog(SerilogHelpers.ConfigureReloadableLogger)
				.UseDefaultServiceProvider(
					(context, options) =>
					{
						var isDevelopment = context.HostingEnvironment.IsDevelopment();
						options.ValidateScopes = isDevelopment;
						options.ValidateOnBuild = isDevelopment;
					})
				.ConfigureWebHost(HostExtension.ConfigureWebHostBuilder)
				.UseConsoleLifetime();
	}
}