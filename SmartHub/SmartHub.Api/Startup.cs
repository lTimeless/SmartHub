using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Api.Extensions;
using SmartHub.Infrastructure.Database;
using System.IO;
using SmartHub.Application;
using SmartHub.Application.UseCases.SignalR;
using SmartHub.Infrastructure;
using SmartHub.Infrastructure.Shared;

namespace SmartHub.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IHostEnvironment AppEnvironment { get; }

        public Startup(IHostEnvironment env, IConfiguration configuration)
        {
            AppEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add each layer
            services.AddInfrastrucurePersistance(Configuration);
            services.AddInfrastrucureShared();
            services.AddApplicationLayer();
            services.AddApiLayer(Configuration, AppEnvironment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedDatabase seedDatabase)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                // Seed Database
                seedDatabase.SeedData(Configuration.GetValue<bool>("Seed_Db")).GetAwaiter().GetResult();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            Log.ForContext(typeof(Startup))
                .Information("----------------------------------------------------------------");
            AppExtension.ShowLocalIpv4();

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

                if (!Configuration.GetValue<bool>("Use_StaticFiles"))
                {
                    Log.ForContext(typeof(Startup)).Warning("Not serving frontend from staticfiles");
                    // Start seperate FE server and Server listens to it
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                    // To start its own FE server
                }
            });
        }
    }
}