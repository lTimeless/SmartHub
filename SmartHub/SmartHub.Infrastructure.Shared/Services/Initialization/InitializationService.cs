using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Infrastructure.Shared.Services.Background;

namespace SmartHub.Infrastructure.Shared.Services.Initialization
{
    public class InitializationService : IInitializationService
    {
        private readonly IHangfireDispatcher _hangfireManager;
        private readonly IHomeFolderService _homeFolderService;
        private readonly ILogger _logger = Log.ForContext(typeof(InitializationService));
        private readonly BackgroundServiceStarter<IChannelManager> _channelManagerStarter;
        private readonly BackgroundServiceStarter<IEventDispatcher> _eventDispatcherStarter;

        public InitializationService(IHangfireDispatcher hangfireManager, IHomeFolderService homeFolderService, BackgroundServiceStarter<IChannelManager> channelManagerStarter, BackgroundServiceStarter<IEventDispatcher> eventDispatcherStarter)
        {
            _hangfireManager = hangfireManager;
            _homeFolderService = homeFolderService;
            _channelManagerStarter = channelManagerStarter;
            _eventDispatcherStarter = eventDispatcherStarter;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Start initialization...");
            await _channelManagerStarter.StartAsync(cancellationToken);
            await _eventDispatcherStarter.StartAsync(cancellationToken);
            // StartHangfireJobs();
            await _homeFolderService.Create().ConfigureAwait(false);
            _logger.Information("Stop initialization.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

            await _channelManagerStarter.StopAsync(cancellationToken);
            await _eventDispatcherStarter.StopAsync(cancellationToken);
            _logger.Information("Stopped all BackgroundServices.");
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
    }
}