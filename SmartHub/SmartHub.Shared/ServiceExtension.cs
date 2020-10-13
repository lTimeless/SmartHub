using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Shared.Services;

namespace SmartHub.Shared
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddServices();
            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}