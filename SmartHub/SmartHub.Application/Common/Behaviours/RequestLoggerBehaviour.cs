using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.Common.Behaviours
{
    public class RequestLoggerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger = Log.ForContext(typeof(RequestLoggerBehaviour<,>));
        private readonly CurrentUser _currentUser;

        public RequestLoggerBehaviour(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;

            _logger.Information("SmartHub Request: {Name} - {@UserName} - {@Request}",
                name, _currentUser.User == null ? _currentUser.RequesterName : _currentUser.User.UserName, request);

            return await next();
        }
    }
}