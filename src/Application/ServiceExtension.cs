using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.UseCases.AppFolder;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Application.UseCases.NetworkScanner;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using System.Reflection;

namespace SmartHub.Application
{
	/// <summary>
	///     Adds Mappers, Services, etc from the application context into the DI container.
	/// </summary>
	public static class ServiceExtension
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
		{
			services.AddAutomapper();
			services.AddServices();
			return services;
		}

		private static void AddAutomapper(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.Load("Application"));
		}

		private static void AddServices(this IServiceCollection services)
		{
			// HomeFolder
			services.AddSingleton<IAppConfigService, AppConfigService>();
			services.AddTransient<IAppFolderService, AppFolderService>();
			// Network
			services.AddScoped<INetworkScannerService, NetworkScannerService>();
			// Geolocation
			services.AddScoped<ILocationService, LocationService>();
			// PluginAdapter
			services.AddScoped<IPluginLoadService, PluginLoadService>();
			services.AddScoped<IPluginCreatorService, PluginCreatorService>();
			services.AddScoped<IPluginHostService, PluginHostService>();
		}
	}
}