using Boxed.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Helpers;
using SmartHub.Domain.Common.Options;
using SmartHub.Infrastructure.Database;
using System;
using System.Reflection;

namespace SmartHub.WebUI.Extensions
{
	public static class HostExtension
	{
		internal static IHost MigrateDatabase(this IHost host)
		{
			using var scope = host.Services.CreateScope();
			using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			try
			{
				// Creates or Updates the database with the newest available migration from
				// "SmartHub.infrastructure.Migrationas"
				appContext.Database.Migrate();
			}
			catch (Exception ex)
			{
				Log.Error($"Error while migrating the DB on startup -- {ex.Message} \n {ex.Source}");
			}

			return host;
		}

		[Obsolete("Not needed because all these settings come from the defaultbuilder")]
		internal static IConfigurationBuilder AddConfiguration(IConfigurationBuilder configurationBuilder,
			IHostEnvironment hostEnvironment, string[] args)
		{
			return configurationBuilder
				// Including appsettings is not needed because the defaultBuilder will do that automatically
				.AddJsonFile("appsettings.json", true, false)
				.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, false)
				.AddIf(hostEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostEnvironment.ApplicationName),
					x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), true, false))
				.AddEnvironmentVariables()
				.AddIf(args is not null, x => x.AddCommandLine(args));
		}

		/// <summary>
		///     Adds default config to the IWebHostBuilder
		/// </summary>
		/// <param name="webHostBuilder">The builder.</param>
		internal static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder)
		{
			webHostBuilder
				.UseKestrel((builderContext, options) =>
				{
					options.AddServerHeader = false;
					options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)),
						false);
					var host = IpAddressUtils.ShowLocalIpv4();
					options.ListenLocalhost(5001, conf => conf.UseHttps());
					options.Listen(host, 5000);
					options.Listen(host, 5001, conf => conf.UseHttps());
				})
				.ConfigureServices(services =>
					services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true))
				.UseStartup<Startup>();
		}
	}
}