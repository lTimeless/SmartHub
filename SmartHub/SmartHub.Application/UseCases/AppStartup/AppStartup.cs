using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.UseCases.Entity.Homes.Create;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.AppStartup
{
	public class AppStartup : IHostedService
	{
		private readonly IHangfireDispatcher _hangfireManager;
		private readonly IHomeFolderService _homeFolderService;
		private readonly IMediator _mediatR;
		private readonly ILogger _log = Log.ForContext(typeof(AppStartup));
		public AppStartup(IHangfireDispatcher hangfireManager,
			IHomeFolderService homeFolderService, IMediator mediatR)
		{
			_hangfireManager = hangfireManager;
			_homeFolderService = homeFolderService;
			_mediatR = mediatR;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{

			// StartHangfireJobs();
			await _homeFolderService.Init().ConfigureAwait(false);
			await StartLoadPlugins().ConfigureAwait(false);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		/// <summary>
		/// This function calls all Hangfire Jobs that start on Server start up
		/// </summary>
		private async Task StartHangfireJobs()
		{
			_log.Information($"[{nameof(StartHangfireJobs)}] Start Hangfire jobs");
			// Only add Job if it doesn't exist 
			//await _hangfireManager.AddRecurringJob(() =>
			//		_pluginAdapterService.StartCompareAndUpdateProcedure(), DateTimeEnum.Minute, _interval).ConfigureAwait(false);
		}

		private async Task StartLoadPlugins()
		{
			await _mediatR.Send(new PluginLoadCommand(LoadStrategy.Multiple));
		}
	}
}
