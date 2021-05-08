using Boxed.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Domain.Common.Constants;
using SmartHub.Domain.Common.Options;
using SmartHub.WebUI.Middleware;
using System;
using System.Linq;

namespace SmartHub.WebUI.Extensions
{
	public static class AppExtension
	{
		public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}


		/// <summary>
		///     Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
		///     headers. The cache duration is controlled from configuration.
		///     See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
		/// </summary>
		/// <param name="application">The application builder.</param>
		/// <returns>The application builder with the static files middleware configured.</returns>
		public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
		{
			var cacheProfile = application
				.ApplicationServices
				.GetRequiredService<CacheProfileOptions>()
				.Where(x => string.Equals(x.Key, CacheProfileNames.StaticFiles, StringComparison.Ordinal))
				.Select(x => x.Value)
				.SingleOrDefault();
			return application
				.UseStaticFiles(
					new StaticFileOptions
					{
						OnPrepareResponse = context =>
						{
							if (cacheProfile is not null)
							{
								context.Context.ApplyCacheProfile(cacheProfile);
							}
						}
					});
		}
	}
}