using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Infrastructure.Shared.Services.Background;
using SmartHub.Infrastructure.Shared.Services.Dispatchers;
using SmartHub.Infrastructure.Shared.Services.FileSystem;
using SmartHub.Infrastructure.Shared.Services.Helpers;
using SmartHub.Infrastructure.Shared.Services.Http;
using SmartHub.Infrastructure.Shared.Services.Initialization;

namespace SmartHub.Infrastructure.Shared
{
    public static class ServiceExtension
    {
        public static void AddInfrastrucureShared(this IServiceCollection services)
        {
            services.AddBackgroundServices();
            services.AddApplicationServices();
            services.AddHelpServices();
        }

        private static void AddBackgroundServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(BackgroundServiceStarter<>));
            services.AddSingleton(typeof(IChannelManager), typeof(ChannelManager));
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddSingleton(typeof(IInitializationService),typeof(InitializationService));
            services.AddHostedService<BackgroundServiceStarter<IInitializationService>>();
        }

        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHangfireDispatcher, HangfireDispatcher>();
            services.AddScoped<IHomeFolderService, HomeFolderService>();
            services.AddScoped<IDirectoryService, DirectoryService>();
            services.AddScoped<IPingService, PingService>();
            services.AddScoped<IHttpService, HttpService>();
        }

        private static void AddHelpServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}