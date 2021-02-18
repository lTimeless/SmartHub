using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application;
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            Log.ForContext(typeof(Startup))
                .Information("----------------------------------------------------------------");
            // Response Compression
            app.UseResponseCompression();
            // Serilog
            app.UseSerilogRequestLogging();
            // CustomExceptionMiddleware
            app.ConfigureCustomExceptionMiddleware();
			// Spa/ StaticFiles
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
	            app.UseSpaStaticFiles();
            }
            // Routing
            app.UseWebSockets();
            app.UseRouting();
            // Auth
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            // Endpoints
            app.UseEndpoints(endpoints =>
            {
	            // Controllers
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");

                // GraphQl
                endpoints.MapGraphQL().WithOptions(
	                new()
	                {
		                Tool = { Enable = false }
	                });
            });
			// Spa
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (Configuration.GetValue<bool>("Use_DevProxy"))
                {
	                // Start separate FE server and Server listens to it
	                spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });
        }
    }
}