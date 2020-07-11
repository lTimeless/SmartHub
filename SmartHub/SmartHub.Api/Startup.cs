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
using SmartHub.Domain.Common.Settings;

namespace SmartHub.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		private IHostEnvironment AppEnvironment { get; set; }
		public Startup(IHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
							.SetBasePath(Directory.GetCurrentDirectory())
							.AddJsonFile("Properties/launchSettings.json", optional: true)
							.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
							.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
							.AddJsonFile("appsettings.Production.json", optional: true)
							.AddJsonFile("smartHub.config.json", optional: false)
							.AddEnvironmentVariables();
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
			Log.Information($"----------------------------------------------------------------");
			app.ShowLocalIpv4();

			// Response Compression
			// needs to be called before 'UseStaticFiles' otherwise these wont be compressed
			app.UseResponseCompression();

			// Serilog
			app.UseSerilogRequestLogging();

			// CustomExceptionMiddleware
			app.ConfigureCustomExceptionMiddleware();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			// Swagger
			app.ConfigureSwagger();

			// Hangfire
			app.ConfigureHangfire();

			// Routing
			app.UseRouting();

			// Auth
			app.UseCors("CorsPolicy");
			app.UseAuthentication();
			app.UseIdentityServer();
			app.UseAuthorization();

			// Endpoints
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "../SmartHub.Ui";

				if (env.IsDevelopment())
				{
					// Start seperate FE server and Server listens to it 
					 spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
					// To start its own FE server
				}
			});
		}
	}
}
