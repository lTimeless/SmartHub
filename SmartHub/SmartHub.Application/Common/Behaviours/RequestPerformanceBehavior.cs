using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Application.Common.Behaviours
{
    public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger = Log.ForContext(typeof(RequestPerformanceBehavior<,>));
        private readonly IUserAccessor _currentUserService;

        public RequestPerformanceBehavior(IUserAccessor currentUserService)
        {
            _timer = new Stopwatch();
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