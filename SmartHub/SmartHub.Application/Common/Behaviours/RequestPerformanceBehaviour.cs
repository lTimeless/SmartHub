using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Application.Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger;
        private readonly IUserAccessor _currentUserService;

        public RequestPerformanceBehaviour(ILogger logger, IUserAccessor currentUserService)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();
            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;
                _logger.Information(
                    "SmartHub Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserName} {@Request}",
                    name, _timer.ElapsedMilliseconds, _currentUserService.GetCurrentUsername(), request);
            }
            return response;
        }
    }
}