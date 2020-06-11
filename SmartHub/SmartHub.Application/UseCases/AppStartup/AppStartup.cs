using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.AppStartup
{
	public class AppStartup : IHostedService
	{
		private readonly IHangfireDispatcher _hangfireManager;

		private readonly IEventDispatcher _eventDispatcher;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;
		// private readonly IPluginLoadService _pluginLoad;
		private readonly IPluginHostService _pluginHostService;
		private readonly IPluginFinderService _pluginFinder;
		private readonly int _interval = 5;

		public AppStartup(IHangfireDispatcher hangfireManager, IEventDispatcher eventDispatcher, IUnitOfWork unitOfWork,
			ILogger logger, IPluginFinderService pluginFinder, IPluginHostService pluginHostService)
		{
			_hangfireManager = hangfireManager;
			_eventDispatcher = eventDispatcher;
			_unitOfWork = unitOfWork;
			_logger = logger;
			_pluginFinder = pluginFinder;
			_pluginHostService = pluginHostService;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			//var t2 = StartHangfireJobs();
			//return Task.WhenAll(t2);

			var home = await _unitOfWork.HomeRepository.GetFirstAsync();
			if (home is null)
			{
				_logger.Information($"[{nameof(AppStartup)}] No home created");
				return;
			}
			var setting = home.Settings.FirstOrDefault(c => c.IsActive || c.PluginPath.Contains("_private"));
			if (setting is null)
			{
				_logger.Information($"[{nameof(AppStartup)}] No plugin path set");
				return;
			}

			await StartLoadPlugins(setting);
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
			_logger.Information($"[{nameof(StartHangfireJobs)}] Start Hangfire jobs");
			// Only add Job if it doesn't exist 
			//await _hangfireManager.AddRecurringJob(() =>
			//		_pluginAdapterService.StartCompareAndUpdateProcedure(), DateTimeEnum.Minute, _interval).ConfigureAwait(false);
		}

		private async Task StartLoadPlugins(Setting setting)
		{
			var foundPlugins = _pluginFinder.FindPluginsInAssemblies(setting.PluginPath);
			var filteredOrAllFoundPlugins = await _pluginFinder.FilterByPluginsInHome(foundPlugins);
			if (filteredOrAllFoundPlugins.IsNullOrEmpty())
			{
				_logger.Warning($"[{nameof(AppStartup)}] No new Plugins loaded");
				return;
			}
			await _pluginHostService.Plugins.LoadAndAddToHomeAsync(new List<string> { setting.PluginPath });
		}
	}
}
