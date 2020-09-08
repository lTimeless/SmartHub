using System;
using Figgle;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Infrastructure.Database;

namespace SmartHub.Api.Extensions
{
	public static class HostExtension
	{
		public static IHost AsciiLogo(this IHost host)
		{
			Console.WriteLine(FiggleFonts.Standard.Render("SmartHub"));
			return host;
		}

		public static IHost WelcomeText(this IHost host)
		{
			Console.WriteLine("Welcome to SmartHub, this is a smarthome written in asp.net core and vue3." + "\n\r" +
				"This is a private project of mine and I use this to learn new things and create my own smarthome that " + "\n\r" +
				"I am going to use myself." + "\n\r" +
				"For more information and if you encounter any issues or have any feedback, please visit: https://github.com/SmartHub-Io/SmartHub ." + "\n\r" +
				"--------------------------------------------------");
			return host;
		}

		public static IHost MigrateDatabase(this IHost host, bool deleteMode)
		{
			using var scope = host.Services.CreateScope();
			using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			var env = host.Services.GetRequiredService<IWebHostEnvironment>();
			try
			{
				// dev Mode
				if (env.IsDevelopment())
				{
					if (deleteMode)
					{
						Log.ForContext(typeof(HostExtension)).Information("Delete database");
						appContext.Database.EnsureDeleted();
					}
					Log.ForContext(typeof(HostExtension)).Information("Update or Create database if needed");

					// adds the current Entity structure
					// appContext.Database.EnsureCreated(); //creates or updates the db if neccassary
				}
				// prod mode
				else
				{
					Log.ForContext(typeof(HostExtension)).Information("Update or Create database if needed");
					// adds the latest Migration from the Migrationsfolder
					// await appContext.Database.MigrateAsync(); //creates or updates the db if neccassary
				}
			}
			catch (Exception ex)
			{
				throw new SmartHubException($"Error while migrating the DB on startup -- {ex.Message} \n {ex.Source}");
			}

			return host;
		}
	}
}
