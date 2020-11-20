using Figgle;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Infrastructure.Services.Dispatchers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Initialization
{
	public class InitializationService : IHostedService 
	{
		private readonly IDbSeeder _dbSeeder;
		private readonly IAppFolderService _homeFolderService;
		private readonly IAppConfigService _configService;
		private readonly ILogger _logger = Log.ForContext(typeof(InitializationService));
		private readonly IChannelManager _channelManagerStarter;
		private readonly EventDispatcher _eventDispatcherStarter;
		private readonly IConfiguration _configuration;

		public InitializationService(IAppFolderService homeFolderService, IChannelManager channelManagerStarter,
			EventDispatcher eventDispatcherStarter, IServiceProvider provider, IConfiguration configuration, IAppConfigService configService)
		{
			_homeFolderService = homeFolderService;
			_channelManagerStarter = channelManagerStarter;
			_eventDispatcherStarter = eventDispatcherStarter;
			using var scope = provider.CreateScope();
			_dbSeeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
			_configuration = configuration;
			_configService = configService;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			WelcomeWithAsciiLogo();
			await _homeFolderService.Create().ConfigureAwait(false);
			_logger.Information("Start initialization...");
			var (homePath, folderName) = _homeFolderService.GetHomeFolderPath();
			_logger.Information("SmartHub folder is at {@homePath}{Seperator}{@folderName}", homePath, Path.DirectorySeparatorChar.ToString(), folderName);
			await _configService.CreateOrReadConfigFile();

			_ = SeedDatabase().ConfigureAwait(true);
			await _channelManagerStarter.StartAsync(cancellationToken);
			await _eventDispatcherStarter.StartAsync(cancellationToken);
			_logger.Information("Stop initialization.");
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await _configService.UpdateFileFromClass();
			await _channelManagerStarter.StopAsync(cancellationToken);
			await _eventDispatcherStarter.StopAsync(cancellationToken);
			_logger.Information("Stopped all BackgroundServices.");
		}


		private void WelcomeWithAsciiLogo()
		{
			_logger.Information($"{Environment.NewLine}" +
								FiggleFonts.Standard.Render("SmartHub"));
			_logger.Information($"{Environment.NewLine}" +
								"Welcome to SmartHub, this is a smarthome written in asp.net core and vue3." + $"{Environment.NewLine}" +
								"This is a private project of mine and I use this to learn new things and create my own smarthome that " + $"{Environment.NewLine}" +
								"I am going to use myself." + $"{Environment.NewLine}" +
								"For more information and if you encounter any issues or have any feedback, please visit: https://github.com/SmartHub-Io/SmartHub." + $"{Environment.NewLine}" +
								"--------------------------------------------------");
		}

		private async Task SeedDatabase()
		{
			if (_configuration.GetValue<bool>("Persistence:Seed_Db"))
			{
				await _dbSeeder.SeedData();
			}
		}
	}
}