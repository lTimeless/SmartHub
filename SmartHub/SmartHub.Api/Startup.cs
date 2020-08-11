using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Api.Extensions;
using SmartHub.Infrastructure.Database;
using System.IO;
using Microsoft.Extensions.Options;
using SmartHub.Application.UseCases.SignalR;
using SmartHub.Domain.Common.Settings;

namespace SmartHub.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		private IHostEnvironment AppEnvironment { get; }
		public Startup(IHostEnvironment env, IConfiguration configuration)
		{
			var builder = new ConfigurationBuilder()
					.AddConfiguration(configuration)
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
					.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
					.AddJsonFile("smarthub.config.json", optional: false)
					;

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets<Startup>();
			}

			AppEnvironment = env;
			Configuration = builder.Build();
		}

		// Autofac DI Container -> runs/builds after ConfigureServices!
		public void ConfigureContainer(ContainerBuilder builder)
		{
			// Register your own things directly with Autofac, like:
			builder.RegisterModule(new AutofacModule());
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.InstallServicesInAssembly(Configuration);

			// -------------- SmartHubSettings ---------------
			services.Configure<ApplicationSettings>(Configuration.GetSection("SmartHub"));
			services.PostConfigure<ApplicationSettings>(options =>
			{
				options.EnvironmentName = AppEnvironment.EnvironmentName;
				options.DefaultName = AppEnvironment.ApplicationName;
			});
			// dann injecten mit IOptions<SmartHubSettings> smartHubSettings -> smartHubSettings.value
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedDatabase seedDatabase)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();

				// Seed Database
				seedDatabase.SeedData().GetAwaiter().GetResult();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			Log.Information("----------------------------------------------------------------");
			app.ShowLocalIpv4();

			// Response Compression
			app.UseResponseCompression();

			// Serilog
			app.UseSerilogRequestLogging();

			// CustomExceptionMiddleware
			app.ConfigureCustomExceptionMiddleware();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			// Swagger
			app.ConfigureSwagger();

			// Hangfire
			app.ConfigureHangfire();

			// Routing
			app.UseRouting();

			// Auth
			app.UseCors("CorsPolicy");
			app.UseAuthentication();
			app.UseAuthorization();

			// Endpoints
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");

				endpoints.MapHub<EventHub>("/api/hub/events");
				endpoints.MapHub<LogHub>("/api/hub/logs");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501
				spa.Options.SourcePath = "wwwroot";

				if (!Configuration.GetValue<bool>("Use_Staticfiles_DEV"))
				{
					Log.Warning("Not serving frontend from staticfiles");
					// Start seperate FE server and Server listens to it
					 spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
					// To start its own FE server
				}
			});
		}
	}
}
