using Figgle;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Initialization
{
	public class InitializationService : IHostedService
	{
		private readonly IAppFolderService _appFolderService;
		private readonly IConfiguration _configuration;
		private readonly IDbSeeder _dbSeeder;
		private readonly ILogger _logger = Log.ForContext(typeof(InitializationService));

		public InitializationService(IAppFolderService homeFolderService, IServiceProvider provider,
			IConfiguration configuration)
		{
			_appFolderService = homeFolderService;
			using var scope = provider.CreateScope();
			_dbSeeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
			_configuration = configuration;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			WelcomeWithAsciiLogo();
			_logger.Information("Start initialization...");

			await _appFolderService.Create();
			var (homePath, folderName) = _appFolderService.GetHomeFolderPath();
			_logger.Information("SmartHub folder is at {HomePath}{Seperator}{FolderName}", homePath,
				Path.DirectorySeparatorChar.ToString(), folderName);

			await SeedDatabaseAsync();
			_logger.Information("Stop initialization");
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await _appFolderService.Save();
			_logger.Information("Stopped all BackgroundServices");
		}


		private void WelcomeWithAsciiLogo()
		{
			_logger.Information("{Environment} {Figgle}", Environment.NewLine, FiggleFonts.Standard.Render("SmartHub"));
			_logger.Information(
				$"{Environment.NewLine}Welcome to SmartHub, this is a smarthome written in asp.net core and vue3." +
				$"{Environment.NewLine}This is a private project of mine and I use this to learn new things and create my own smarthome that " +
				$"{Environment.NewLine}I am going to use myself." + $"{Environment.NewLine}" +
				"For more information and if you encounter any issues or have any feedback, please visit: https://github.com/SmartHub-Io/SmartHub." +
				$"{Environment.NewLine}--------------------------------------------------");
		}

		private async Task SeedDatabaseAsync()
		{
			if (_configuration.GetValue<bool>("Persistence:Seed_Db"))
			{
				await _dbSeeder.SeedData();
			}
		}
	}
}