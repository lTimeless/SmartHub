using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
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
	}
}