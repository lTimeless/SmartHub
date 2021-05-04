using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHub.Infrastructure.Database;
using System;
using System.Reflection;
using Boxed.AspNetCore;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using SmartHub.Domain.Common.Options;

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

		/// <summary>
		/// For more info about the creation of the "CreateDefaultBuilder" see "https://docs.microsoft.com/de-de/aspnet/core/fundamentals/host/web-host?view=aspnetcore-5.0".
		/// </summary>
		/// <param name="configurationBuilder">The builder.</param>
		/// <param name="hostEnvironment">The host environment.</param>
		/// <param name="args">The cmd args.</param>
		/// <returns>The builder with added config.</returns>
		internal static IConfigurationBuilder AddConfiguration(IConfigurationBuilder configurationBuilder,
			IHostEnvironment hostEnvironment, string[] args) =>
			configurationBuilder
				// Including appsettings is not needed because the defaultBuilder will do that automatically
				.AddJsonFile("appsettings.json", true, false)
				.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, false)
				.AddIf(hostEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostEnvironment.ApplicationName),
					x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), true, false))
				.AddEnvironmentVariables()
				.AddIf(args is not null, x => x.AddCommandLine(args));

		/// <summary>
		/// Adds default config to the IWebHostBuilder
		/// </summary>
		/// <param name="webHostBuilder">The builder.</param>
		internal static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder) =>
			webHostBuilder
				.UseKestrel((builderContext, options) =>
				{
					options.AddServerHeader = false;
					options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)),
						false);
					// var host = IpAddressUtils.ShowLocalIpv4();
					// webBuilder.UseKestrel(opt =>
					// {
					//  opt.ListenLocalhost(5001, conf => conf.UseHttps());
					//  opt.Listen(host, 5000);
					//  opt.Listen(host, 5001, conf => conf.UseHttps());
					//
					// });
				})
				.ConfigureServices(services =>
					services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true))
				.UseStartup<Startup>();
	}
}