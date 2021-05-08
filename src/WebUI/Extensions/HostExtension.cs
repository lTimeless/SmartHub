using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Helpers;
using SmartHub.Infrastructure.Database;
using System;

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
				// "SmartHub.infrastructure.Migrations"
				appContext.Database.Migrate();
			}
			catch (Exception ex)
			{
				Log.Error("Error while migrating the DB on startup -- {Message} \n {Source}",
					ex.Message, ex.Source);
			}

			return host;
		}

		/// <summary>
		///     Adds default config to the IWebHostBuilder
		/// </summary>
		/// <param name="webHostBuilder">The builder.</param>
		internal static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder)
		{
			var host = IpAddressUtils.ShowLocalIpv4();
			webHostBuilder.UseKestrel((_, options) =>
				{
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