using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHub.Infrastructure.Database;
using System;
using System.IO;
using System.Reflection;
using Boxed.AspNetCore;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SmartHub.Domain.Common.Options;

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
                // "SmartHub.infrastructure.Persistence/Migrations"
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error($"Error while migrating the DB on startup -- {ex.Message} \n {ex.Source}");
            }

            return host;
        }

        internal static IConfigurationBuilder AddConfiguration(IConfigurationBuilder configurationBuilder,
            IHostEnvironment hostEnvironment, string[] args) =>
            configurationBuilder
                .AddJsonFile("appsettings.json", true, false)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, false)
                // Add configuration from files in the specified directory. The name of the file is the key and the contents the value.
                .AddKeyPerFile(Path.Combine(Directory.GetCurrentDirectory(), "configuration"), true, false)
                .AddIf(hostEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostEnvironment.ApplicationName),
                    x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), true, false))
                .AddEnvironmentVariables()
                .AddIf(args is not null, x => x.AddCommandLine(args));

        internal static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder) =>
            webHostBuilder
                .UseKestrel(
                    (builderContext, options) =>
                    {
                        options.AddServerHeader = false;
                        options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)),
                            false);
                    })
                // Used for IIS and IIS Express for in-process hosting. Use UseIISIntegration for out-of-process hosting.
                .UseIIS()
                .ConfigureServices(
                    services => services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true))
                .UseStartup<Startup>();

    }
}
