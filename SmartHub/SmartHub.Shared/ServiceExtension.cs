using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}