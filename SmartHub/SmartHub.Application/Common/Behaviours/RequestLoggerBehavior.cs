using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.SignalR.Services;
using SmartHub.Domain.Common.Enums;
using Activity = SmartHub.Application.Common.Models.Activity;

namespace SmartHub.Application.Common.Behaviours
{
    public class RequestLoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger = Log.ForContext(typeof(RequestLoggerBehavior<,>));
        private readonly CurrentUser _currentUser;
        private readonly ISendOverSignalR _sendOverSignalR;

        public RequestLoggerBehavior(CurrentUser currentUser, ISendOverSignalR sendOverSignalR)
        {
            _currentUser = currentUser;
            _sendOverSignalR = sendOverSignalR;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;
            var userName = _currentUser.RequesterName;

            if (userName == Roles.System.ToString() && (name == "LoginQuery" || name == "RegisRegistrationCommand" || name == "CheckHomeQuery" || name == "CheckUsersQuery" ))
            {
                userName = "Anonymous";
            }

            var act = new Activity(DateTime.Now.ToString("HH:mm:ss"),
                userName,
                $"{name} started.",
                _timer.ElapsedMilliseconds);
            await _sendOverSignalR.SendActivity(act);

            _timer.Start();
            var response = await next();
            _timer.Stop();

            var successProp = response?.GetType().GetProperty("Success")?.GetValue(response) as bool?;
            var successMessage = response?.GetType().GetProperty("Message")?.GetValue(response) as string;

            if (_timer.ElapsedMilliseconds > 500)
            {
                _logger.Warning(
                    "Long Request: {Name} - {@UserName} - {@Request} [{ElapsedMilliseconds} milliseconds]",
                    name, userName, request, _timer.ElapsedMilliseconds );
            }
            else
            {
                _logger.Information("Request: {Name} - {@UserName} - {@Request}",
                    name, userName, request);
            }

            act = new Activity(DateTime.Now.ToString("HH:mm:ss"),
                userName,
                $"{name} finished: {successMessage}",
                _timer.ElapsedMilliseconds,
                successProp);
            await _sendOverSignalR.SendActivity(act);
            return response;
        }
    }
}