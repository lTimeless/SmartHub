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
        public static IServiceCollection AddInfrastructureShared(this IServiceCollection services)
        {
            services.AddBackgroundServices();
            services.AddApplicationServices();
            services.AddHelpServices();
            return services;
        }

        private static void AddBackgroundServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(BackgroundServiceStarter<>));
            services.AddSingleton<IChannelManager,ChannelManager>();
            services.AddSingleton<IHangfireDispatcher, HangfireDispatcher>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddSingleton(typeof(IInitializationService),typeof(InitializationService));
            services.AddHostedService<BackgroundServiceStarter<IInitializationService>>();
        }

        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IHomeFolderService, HomeFolderService>();
            services.AddTransient<IDirectoryService, DirectoryService>();
            services.AddScoped<IPingService, PingService>();
            services.AddScoped<IHttpService, HttpService>();
        }

        private static void AddHelpServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}