using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Hosting;
using SmartHub.Application.Common.Helpers;
using SmartHub.WebUI.Extensions;
using SmartHub.WebUI.Serilog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartHub.WebUI
{
	public sealed class Program
	{
		public static async Task<int> Main(string[] args)
		{
			Log.Logger = CreateBootstrapLogger();
			IHostEnvironment? hostEnvironment = null;

			try
			{
				Log.Information("Initialising.");
				var host = CreateHostBuilder(args).Build();
				hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
				hostEnvironment.ApplicationName = AssemblyInformation.Current.Product;

				Log.Information(
					"Started {Application} in {Environment} mode.",
					hostEnvironment.ApplicationName,
					hostEnvironment.EnvironmentName);

				await CreateHostBuilder(args)
					.Build()
					.MigrateDatabase()
					.RunAsync()
					.ConfigureAwait(false);
				Log.Information(
					"Stopped {Application} in {Environment} mode.",
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

		/// <summary>
		/// Creates a logger used during application initialisation.
		/// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
		/// </summary>
		/// <returns>A logger that can load a new configuration.</returns>
		private static ReloadableLogger CreateBootstrapLogger() =>
			new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.Debug()
				.CreateBootstrapLogger();

		/// <summary>
		/// Configures a logger used during the applications lifetime.
		/// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
		/// </summary>
		private static void ConfigureReloadableLogger(
			HostBuilderContext context,
			IServiceProvider services,
			LoggerConfiguration configuration) =>
			configuration
				.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services)
				.Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
				.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
				.WriteTo.Conditional(
					x => context.HostingEnvironment.IsDevelopment(),
					x => x.Console().WriteTo.Debug());
	}
}
