using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.Common.Behaviours;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Application.UseCases.Identity;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.Identity.Registration;
using SmartHub.Application.UseCases.NetworkScanner;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Application.UseCases.PluginAdapter.Loader;

namespace SmartHub.Application
{
    public static class ServiceExtension
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutomapper();
            services.AddMediatr();
            services.AddServices();
        }

        private static void AddAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("Smarthub.Application"));
        }

        private static void AddServices(this IServiceCollection services)
        {
            // Identity
            services.AddScoped<CurrentUser>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IdentityService>();
            // Network
            services.AddScoped<INetworkScannerService, NetworkScannerService>();
            // Geolocation
            services.AddScoped<ILocationService, LocationService>();
            // PluginAdapter
            services.AddScoped<IPluginLoadService, PluginLoadService>();
            services.AddScoped<IPluginCreatorService, PluginCreatorService>();
            services.AddScoped<IPluginHostService, PluginHostService>();
        }

        private static void AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.Load("SmartHub.Application"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CurrentUserBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggerBehaviour<,>));
        }
    }
}