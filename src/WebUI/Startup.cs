using Boxed.AspNetCore;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application;
using SmartHub.Domain.Common.Constants;
using SmartHub.Infrastructure;
using SmartHub.WebUI.Extensions;

namespace SmartHub.WebUI
{
	public class Startup
	{
		private IConfiguration Configuration { get; }
		private IHostEnvironment HostEnvironment { get; }

		public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
		{
			Configuration = configuration;
			HostEnvironment = hostEnvironment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) =>
			services
				.AddDatabaseDeveloperPageExceptionFilter()
				.AddInfrastructurePersistence(Configuration)
				.AddApplicationLayer()
				.AddApiLayer(Configuration);

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app">The application builder.</param>
		public void Configure(IApplicationBuilder app)
		{
			app.UseIf(HostEnvironment.IsDevelopment(), x => x.UseServerTiming());
			app.UseIfElse(HostEnvironment.IsDevelopment(),
				x => x.UseDeveloperExceptionPage().UseMigrationsEndPoint(),
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				x => x.UseExceptionHandler("/Error").UseHsts()
			);
			Log.ForContext(typeof(Startup))
				.Information("----------------------------------------------------------------");
			// ForwardedHeaders
			app.UseForwardedHeaders();
			// Serilog
			app.UseSerilogRequestLogging();
			// CustomExceptionMiddleware
			app.UseCustomExceptionMiddleware();
			// Spa/ StaticFiles
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			if (!HostEnvironment.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			// Routing
			app.UseWebSockets();
			app.UseRouting();
			// Response Compression
			app.UseResponseCompression();
			// Auth
			app.UseCors("CorsPolicy");
			app.UseAuthentication();
			app.UseAuthorization();
			// Endpoints
			app.UseEndpoints(builder =>
			{
				// Controllers
				builder.MapControllerRoute(
					"default",
					"{controller}/{action=Index}/{id?}");
				// GraphQl
				builder.MapGraphQL()
					.WithOptions(new() {Tool = {Enable = HostEnvironment.IsDevelopment()}});
				builder
					.MapHealthChecks("/status")
					.RequireCors(CorsPolicyNames.AllowAny);
				builder
					.MapHealthChecks("/status/self", new HealthCheckOptions() {Predicate = _ => false})
					.RequireCors(CorsPolicyNames.AllowAny);
			});
			// Spa
			app.UseSpa(builder =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501
				builder.Options.SourcePath = "ClientApp";

				if (Configuration.GetValue<bool>("Use_DevProxy"))
				{
					// Start separate FE server and Server listens to it
					builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
				}
			});
		}
	}
}