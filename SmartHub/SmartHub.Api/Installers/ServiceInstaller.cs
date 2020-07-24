﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppStartup;
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

namespace SmartHub.Api.Installers
{
	public class ServicesInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			ConfigureRepositories(services);
			ConfigureBackgroundServices(services);
			ConfigureApplicationStartupServices(services);
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
			services.AddHostedService<AppStartup>();
		}

		private static void ConfigureBackgroundServices(IServiceCollection services)
		{
			// services.AddHostedService<EventHub>();
			services.AddSingleton(typeof(IChannelManager), typeof(ChannelManager));
			services.AddHostedService<BackgroundServiceStarter<IChannelManager>>();
			services.AddSingleton<IEventDispatcher, EventDispatcher>();
			services.AddHostedService<BackgroundServiceStarter<IEventDispatcher>>();
			// services.AddHostedService<BackgroundServiceStarter<HomeFolderService>>();
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

			services.AddScoped(typeof(IPluginLoadService<>), typeof(PluginLoadService<>));
			services.AddScoped<IPluginFinderService, PluginFinderService>();
			services.AddScoped(typeof(IPluginCreatorService<>), typeof(PluginCreatorService<>));
			services.AddScoped(typeof(IPluginHostService), typeof(PluginHostService));
			services.AddScoped<IDirectoryService, DirectoryService>();
		}
	}
}
