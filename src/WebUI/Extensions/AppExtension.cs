using Microsoft.AspNetCore.Builder;
using SmartHub.WebUI.Middleware;

namespace SmartHub.WebUI.Extensions
{
	public static class AppExtension
	{
		public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
