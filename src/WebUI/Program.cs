using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SmartHub.WebUI.Extensions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartHub.WebUI
{
	public class Program
	{
		public static async Task<int> Main(string[] args)
		{
			Log.Logger = SerilogExtension.CreateBootstrapLogger();
			try
			{
				var host = CreateHostBuilder(args).Build();
				var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
				hostEnvironment.ApplicationName = AssemblyInformation.Current.Product;
				await host.MigrateDatabase().RunAsync();
				Log.Information("Stopped {Application}", hostEnvironment.ApplicationName);
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
				.UseSerilog(SerilogExtension.ConfigureReloadableLogger)
				.ConfigureLogging((_, config) => config.ClearProviders())
				.ConfigureWebHost(HostExtension.ConfigureWebHostBuilder);
		}
	}
}