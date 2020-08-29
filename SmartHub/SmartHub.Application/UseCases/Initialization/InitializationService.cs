using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.Initialization
{
    public class InitializationService : IHostedService
    {
        private readonly IHangfireDispatcher _hangfireManager;
        private readonly IHomeFolderService _homeFolderService;
        private readonly IMediator _mediatR;
        private readonly ILogger _logger = Log.ForContext(typeof(InitializationService));

        public InitializationService(IMediator mediatR, IHangfireDispatcher hangfireManager, IHomeFolderService homeFolderService)
        {
            _mediatR = mediatR;
            _hangfireManager = hangfireManager;
            _homeFolderService = homeFolderService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Start initialization...");
            // StartHangfireJobs();
            await _homeFolderService.Init().ConfigureAwait(false);
            await StartLoadPlugins().ConfigureAwait(false);
            _logger.Information("Stop initialization.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromCanceled(cancellationToken);
        }

        /// <summary>
        /// This function calls all Hangfire Jobs that start on Server start up
        /// </summary>
        private async Task StartHangfireJobs()
        {
            _logger.Information("Start Hangfire jobs");
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