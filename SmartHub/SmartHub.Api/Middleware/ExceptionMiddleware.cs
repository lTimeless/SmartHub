using Microsoft.AspNetCore.Http;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;

namespace SmartHub.Api.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly ILogger _log = Log.ForContext(typeof(ExceptionMiddleware));
		private readonly IUnitOfWork _unitOfWork;
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next, IUnitOfWork unitOfWork)
		{
			_next = next;
			_unitOfWork = unitOfWork;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
		{
			object? errors;
			await _unitOfWork.RollbackAsync();
			_log.Warning("Rollback all changes from this request {}.", httpContext.TraceIdentifier);
			switch (ex)
			{
				case RestException restException:
					_log.Warning($"Rest ERROR: {restException.Code} -- {restException.Errors}");
					httpContext.Response.StatusCode = (int)restException.Code;
					errors = restException.Errors;
					break;

				case SmartHubException smartHubException:
					_log.Warning($"SmartHub ERROR: {smartHubException.Message} -- {smartHubException.Source} -- {smartHubException.StackTrace}");
					errors = string.IsNullOrWhiteSpace(smartHubException.Message) ? "Error" : smartHubException.Message;
					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;

				case Exception exception:
					_log.Warning($"Server ERROR: {exception}");
					errors = string.IsNullOrWhiteSpace(exception.Message) ? "Error" : exception.Message;
					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;

				default:
					_log.Warning("Unknown Server ERROR");
					errors = string.IsNullOrWhiteSpace("Unknown Server ERROR");
					break;
			}

			httpContext.Response.ContentType = "application/json";
			if (errors != null)
			{
				var result = JsonSerializer.Serialize(
					Response.Fail("Server Error", errors)
				);
				await httpContext.Response.WriteAsync(result);
			}
		}
	}
}
