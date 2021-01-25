using System;

namespace SmartHub.Domain.Common.Extensions
{
	public static class UriExtension
	{
		public static string ToStringWithoutTrailingSlash(this UriBuilder uriBuilder)
		{
			return uriBuilder.ToString().TrimEnd('/');
		}

		public static bool IsHttp(this Uri uri)
		{
			return uri.Scheme == "http" || uri.Scheme == "https";
		}
	}
}