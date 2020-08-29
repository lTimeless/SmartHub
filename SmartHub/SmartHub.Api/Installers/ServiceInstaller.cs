using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.GeoLocation;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Application.UseCases.Identity;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.Identity.Registration;
using SmartHub.Application.UseCases.NetworkScanner;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.Infrastructure.Database;
using SmartHub.Infrastructure.Database.Repositories;
using SmartHub.Infrastructure.Services.Auth;
using SmartHub.Infrastructure.Services.Background;
using SmartHub.Infrastructure.Services.Dispatchers;
using SmartHub.Infrastructure.Services.FileSystem;
using SmartHub.Infrastructure.Services.Http;
using SmartHub.Infrastructure.Services.Initialization;

namespace SmartHub.Api.Installers
{
	public class ServicesInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			ConfigureRepositories(services);
			ConfigureApplicationStartupServices(services);
			ConfigureBackgroundServices(services);
			ConfigureAuthServices(services);
			ConfigureHelpServices(services);
			ConfigureServices(services);
		}

		private static void ConfigureRepositories(IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IHomeRepository, HomeRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddTransient<SeedDatabase>();
		}

		private static void ConfigureApplicationStartupServices(IServiceCollection services)
		{
			services.AddScoped<IHangfireDispatcher, HangfireDispatcher>();
			services.AddScoped<IHomeFolderService, HomeFolderService>();
		}

		private static void ConfigureBackgroundServices(IServiceCollection services)
		{
			services.AddTransient(typeof(BackgroundServiceStarter<>));
			services.AddSingleton(typeof(IChannelManager), typeof(ChannelManager));
			services.AddSingleton<IEventDispatcher, EventDispatcher>();
			services.AddSingleton(typeof(IInitializationService),typeof(InitializationService));
			services.AddHostedService<BackgroundServiceStarter<IInitializationService>>();
		}

		private static void ConfigureAuthServices(IServiceCollection services)
		{
			services.AddScoped<CurrentUser>();
			services.AddScoped<ITokenGenerator, TokenGenerator>();
			services.AddScoped<ILoginService, LoginService>();
			services.AddScoped<IRegistrationService, RegistrationService>();
			services.AddScoped<IUserAccessor, UserAccessor>();
			services.AddScoped<IdentityService>();
		}

		private static void ConfigureHelpServices(IServiceCollection services)
		{
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<INetworkScannerService, NetworkScannerService>();
			services.AddScoped<IPingService, PingService>();
			services.AddScoped<IHttpService, HttpService>();
			services.AddScoped<ILocationService, LocationService>();

			services.AddScoped(typeof(IPluginLoadService<>), typeof(PluginLoadService<>));
			services.AddScoped<IPluginFinderService, PluginFinderService>();
			services.AddScoped(typeof(IPluginCreatorService<>), typeof(PluginCreatorService<>));
			services.AddScoped(typeof(IPluginHostService), typeof(PluginHostService));
			services.AddScoped<IDirectoryService, DirectoryService>();
		}
	}
}
