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
        private readonly IHomeFolderService _homeFolderService;
        private readonly ILogger _logger = Log.ForContext(typeof(InitializationService));
        private readonly BackgroundServiceStarter<IChannelManager> _channelManagerStarter;
        private readonly BackgroundServiceStarter<IEventDispatcher> _eventDispatcherStarter;

        public InitializationService(IHomeFolderService homeFolderService, BackgroundServiceStarter<IChannelManager> channelManagerStarter, BackgroundServiceStarter<IEventDispatcher> eventDispatcherStarter)
        {
            _homeFolderService = homeFolderService;
            _channelManagerStarter = channelManagerStarter;
            _eventDispatcherStarter = eventDispatcherStarter;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Start initialization...");
            await _channelManagerStarter.StartAsync(cancellationToken);
            await _eventDispatcherStarter.StartAsync(cancellationToken);
            await _homeFolderService.Create().ConfigureAwait(false);
            _logger.Information("Stop initialization.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _channelManagerStarter.StopAsync(cancellationToken);
            await _eventDispatcherStarter.StopAsync(cancellationToken);
            _logger.Information("Stopped all BackgroundServices.");
        }
    }
}