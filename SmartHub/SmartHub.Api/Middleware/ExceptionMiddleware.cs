using Microsoft.AspNetCore.Http;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartHub.Api.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
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
			object? errors = null;
			// TODO: call unitofWork callback function
			switch (ex)
			{
				case RestException restException:
					Log.Warning($"[{nameof(HandleExceptionAsync)}] Rest ERROR: {restException.Code} -- {restException.Errors}");
					httpContext.Response.StatusCode = (int)restException.Code;
					errors = restException.Errors;
					break;

				case SmartHubException smartHubException:
					Log.Warning($"[{nameof(HandleExceptionAsync)}] SmartHub ERROR: {smartHubException.Message} -- {smartHubException.Source}");
					errors = string.IsNullOrWhiteSpace(smartHubException.Message) ? "Error" : smartHubException.Message;
					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;

				case Exception exception:
					Log.Warning($"[{nameof(HandleExceptionAsync)}] Server ERROR: {exception}");
					errors = string.IsNullOrWhiteSpace(exception.Message) ? "Error" : exception.Message;
					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;

				default:
					Log.Warning($"[{nameof(HandleExceptionAsync)}] Unknown Server ERROR");
					break;
			}

			httpContext.Response.ContentType = "application/json";

			if (errors != null)
			{
				var result = JsonSerializer.Serialize(new
				{
					errors
				});
				await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
			}
		}
	}
}
