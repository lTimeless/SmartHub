using Microsoft.AspNetCore.Builder;
using SmartHub.WebUI.Middleware;

namespace SmartHub.WebUI.Extensions
{
	public static class AppExtension
	{
		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
