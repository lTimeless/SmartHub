using System.Reflection;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.Common.Behaviours;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Application.UseCases.HomeFolder.HomeConfigParser;
using SmartHub.Application.UseCases.Identity;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.Identity.Registration;
using SmartHub.Application.UseCases.NetworkScanner;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.Application.UseCases.SignalR.Services;

namespace SmartHub.Application
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutomapper();
            services.AddMediatr();
            services.AddServices();
            return services;
        }

        private static void AddAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("Smarthub.Application"));
        }

        private static void AddServices(this IServiceCollection services)
        {
            // HomeFolder
            services.AddTransient<IConfigService, ConfigService>();
            services.AddTransient<IHomeFolderService, HomeFolderService>();
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
            // SignalR services
            services.AddTransient<ISendOverSignalR, SendOverSignalR>();
        }

        private static void AddMediatr(this IServiceCollection services)
        {
            // The Pre/Postprocessors are loaded automatically into DI-container
            services.AddMediatR(Assembly.Load("SmartHub.Application"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggerBehaviour<,>));
        }
    }
}