using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.Domain.Enums;

namespace SmartHub.Application.UseCases.AppStartup
{
	public class AppStartup : IHostedService
	{
		private readonly IHangfireDispatcher _hangfireManager;
		private readonly IEventDispatcher _eventDispatcher;
		private readonly IHomeFolderService _homeFolderService;
		private readonly IMediator _mediatR;

		public AppStartup(IHangfireDispatcher hangfireManager, IEventDispatcher eventDispatcher,
			IHomeFolderService homeFolderService, IMediator mediatR)
		{
			_hangfireManager = hangfireManager;
			_eventDispatcher = eventDispatcher;
			_homeFolderService = homeFolderService;
			_mediatR = mediatR;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			//var t2 = StartHangfireJobs();
			//return Task.WhenAll(t2);
			await _eventDispatcher.Init().ConfigureAwait(false);
			await _hangfireManager.Init().ConfigureAwait(false);
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
			Log.Information($"[{nameof(StartHangfireJobs)}] Start Hangfire jobs");
			// Only add Job if it doesn't exist 
			//await _hangfireManager.AddRecurringJob(() =>
			//		_pluginAdapterService.StartCompareAndUpdateProcedure(), DateTimeEnum.Minute, _interval).ConfigureAwait(false);
		}

		private async Task StartLoadPlugins()
		{
			_ = await _mediatR.Send(new PluginLoadCommand(LoadStrategy.Multiple));
		}
	}
}
