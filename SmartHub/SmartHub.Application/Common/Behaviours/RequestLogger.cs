using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SmartHub.Application.Common.Interfaces;
using Serilog;

namespace SmartHub.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IUserAccessor _userAccessor;

        public RequestLogger(ILogger logger, IUserAccessor userAccessor)
        {
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.Information("SmartHub Request: {Name} - {@UserId} - {@Request}",
                name, _userAccessor.GetCurrentUsername(), request);

            return Task.CompletedTask;
        }
    }
}