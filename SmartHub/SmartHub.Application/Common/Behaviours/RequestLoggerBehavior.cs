using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Behaviours
{
    public class RequestLoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger = Log.ForContext(typeof(RequestLoggerBehavior<,>));
        private readonly CurrentUser _currentUser;

        public RequestLoggerBehavior(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;

            _logger.Information("SmartHub Request: {Name} - {@UserName} - {@Request}",
                name, _currentUser.User == null ? _currentUser.RequesterName : _currentUser.User.UserName, request);
            //var response = next();
            // die respone ist mein Response class mit den daten darain
            // von dem kann ich dann erfahren ob die request erfolgreich war oder nicht
            // also response nur als Response casten und das wars

            // wenn currentUser == system dann schau im request nach dem Feld "UserName" und wenn das auch null ist dann
            // ist der request anonym gewesen

            var response = await next();
            var successProp = response?.GetType().GetProperty("Success");
            var successMessage = response?.GetType().GetProperty("Message");

            // dann Logstatemant aufbauen:
            // [Datetime] {Username} {Message} [ExecutionTime]
            // die ganze logic dann in "requestPerformanceBehaviour" packen
            // dann hier publishen als Event und dann der dispatcher baut daraus dann ein richtiges Event object
            //

            return response;
        }
    }
}