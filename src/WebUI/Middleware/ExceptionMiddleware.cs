using Microsoft.AspNetCore.Http;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartHub.WebUI.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly ILogger _log = Log.ForContext(typeof(ExceptionMiddleware));
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext, IUnitOfWork unitOfWork)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex, unitOfWork).ConfigureAwait(false);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex, IUnitOfWork unitOfWork)
		{
			object? errors;
			await unitOfWork.RollbackAsync();
			_log.Warning("Rollback all changes from this request {}.", httpContext.Request);
			switch (ex)
			{
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
					new ExceptionPayload(new($"{errors}", AppErrorCodes.ServerError)));
				await httpContext.Response.WriteAsync(result);
			}
		}
	}
}
