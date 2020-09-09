using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SmartHub.Infrastructure.Shared.Services.Background
{
    /// <summary>
    /// This Service handles background services
    /// </summary>
    /// <typeparam name="T">The Service the start in Background</typeparam>
    public class BackgroundServiceStarter<T> : IHostedService where T : IHostedService
    {
        private readonly T _backgroundService;

        public BackgroundServiceStarter(T backgroundService)
        {
            _backgroundService = backgroundService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _backgroundService.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _backgroundService.StopAsync(cancellationToken);
        }
    }
}