using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Activities;
using SmartHub.Application.UseCases.SignalR.Services;

namespace SmartHub.Application.Common.Behaviours
{
    /// <summary>
    /// Logs and stops the time for the current Request
    /// </summary>
    public class RequestLoggerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger = Log.ForContext(typeof(RequestLoggerBehaviour<,>));
        private readonly CurrentUser _currentUser;
        private readonly ISendOverSignalR _sendOverSignalR;
        private const int MaxElapsableTime = 500;

        public RequestLoggerBehaviour(CurrentUser currentUser, ISendOverSignalR sendOverSignalR)
        {
            _currentUser = currentUser;
            _sendOverSignalR = sendOverSignalR;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            var userName = _currentUser.RequesterName;

            _timer.Start();
            var response = await next();
            _timer.Stop();

            var successProp = response?.GetType().GetProperty("Success")?.GetValue(response) as bool? ?? false;
            var message = response?.GetType().GetProperty("Message")?.GetValue(response) as string;

            if (_timer.ElapsedMilliseconds > MaxElapsableTime)
            {
                _logger.Warning(
                    "Long Request: {Name} - {@UserName} - {@Request} [{ElapsedMilliseconds} milliseconds]",
                    requestName, userName, request, _timer.ElapsedMilliseconds);
            }
            else
            {
                _logger.Information("Request: {Name} - {@UserName} - {@Request}",
                    requestName, userName, request);
            }

            await _sendOverSignalR.SendActivity(userName, $"{requestName}", $"{requestName} executed: {message}" ,_timer.ElapsedMilliseconds, successProp);
            return response;
        }
    }
}