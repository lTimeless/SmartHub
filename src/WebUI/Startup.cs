using Boxed.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

		/// <summary>
		///     This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">The Service DI-container.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddDatabaseDeveloperPageExceptionFilter()
				.AddInfrastructurePersistence(Configuration)
				.AddApplicationLayer()
				.AddApiLayer(HostEnvironment, Configuration);
		}

		/// <summary>
		///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
			// ForwardedHeaders
			// app.UseForwardedHeaders();
			// Serilog
			app.UseCustomSerilogRequestLogging();
			// CustomExceptionMiddleware
			app.UseCustomExceptionMiddleware();
			// Spa/ StaticFiles
			app.UseHttpsRedirection()
				.UseStaticFilesWithCacheControl()
				.UseIf(!HostEnvironment.IsDevelopment(), x => app.UseCustomSpaFiles());
			// Routing & ResponseCompression
			app.UseWebSockets()
				.UseRouting()
				.UseResponseCompression();
			// Auth
			app.UseCors(AuthConstants.CorsPolicies.AllowAny)
				.UseAuthentication()
				.UseAuthorization();
			// Endpoints
			app.UseEndpoints(builder =>
			{
				// Controllers
				builder.MapControllers();
				// GraphQl
				builder.MapGraphQL()
					.WithOptions(new() {Tool = {Enable = HostEnvironment.IsDevelopment()}});
				// HealthChecks
				builder
					.MapHealthChecks("/status")
					.RequireCors(AuthConstants.CorsPolicies.AllowAny);
				builder
					.MapHealthChecks("/status/self", new() {Predicate = _ => false})
					.RequireCors(AuthConstants.CorsPolicies.AllowAny);
			});
			// Spa
			app.UseSpa(builder =>
			{
				// To learn more about options for serving an SPA from ASP.NET Core,
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