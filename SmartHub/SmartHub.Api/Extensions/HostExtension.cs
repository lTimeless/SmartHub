using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Infrastructure.Database;

namespace SmartHub.Api.Extensions
{
	public static class HostExtension
	{
		public static IHost MigrateDatabase(this IHost host)
		{
			using var scope = host.Services.CreateScope();
			using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			try
			{
				// Creates or Updates the database with the newest available migration from
				// "SmartHub.infrastructure.Persistence/Migrations"
				appContext.Database.Migrate();
			}
			catch (Exception ex)
			{
				throw new SmartHubException($"Error while migrating the DB on startup -- {ex.Message} \n {ex.Source}");
			}
			return host;
		}
	}
}