using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.UseCases.AppStartup;
using SmartHub.Application.UseCases.Identity.Login;
using SmartHub.Application.UseCases.Identity.Registration;
using SmartHub.Application.UseCases.NetworkScanner;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Entities.Users;
using SmartHub.Infrastructure.Database;
using SmartHub.Infrastructure.Database.Repositories;
using SmartHub.Infrastructure.Services.Auth;
using SmartHub.Infrastructure.Services.DateTime;
using SmartHub.Infrastructure.Services.Dispatchers;
using SmartHub.Infrastructure.Services.Http;

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
			services.AddScoped<IHomeRepository, HomeRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddTransient<SeedDatabase>();
		}

		private static void ConfigureApplicationStartupServices(IServiceCollection services)
		{
			services.AddSingleton(typeof(IChannelManager), typeof(ChannelManager));
			services.AddSingleton<IEventDispatcher, EventDispatcher>();
			services.AddScoped<IHangfireDispatcher, HangfireDispatcher>();
			services.AddHostedService<AppStartup>();
		}

		private static void ConfigureBackgroundServices(IServiceCollection services)
		{
			//services.AddHostedService<HubBackgroundService>();
			// services.AddHostedService<ConnectionBackgroundService>();
		}

		private static void ConfigureAuthServices(IServiceCollection services)
		{
			services.AddScoped<ITokenGenerator, TokenGenerator>();
			services.AddScoped<ILoginService, LoginService>();
			services.AddScoped<IRegistrationService, RegistrationService>();
			services.AddScoped<IUserAccessor, UserAccessor>();
			services.AddScoped<IUserService, UserService>();
		}

		private static void ConfigureHelpServices(IServiceCollection services)
		{
			services.AddTransient<IDateTimeService, DateTimeService>();
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<INetworkScannerService, NetworkScannerService>();
			services.AddScoped<IPingService, PingService>();
			services.AddScoped<IHttpService, HttpService>();

			services.AddScoped(typeof(IPluginLoadService<>), typeof(PluginLoadService<>));
			services.AddScoped<IPluginFinderService, PluginFinderService>();
			services.AddScoped(typeof(IPluginCreatorService<>), typeof(PluginCreatorService<>));
			services.AddScoped(typeof(IPluginHostService), typeof(PluginHostService));
		}
	}
}
