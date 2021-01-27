using System;

namespace SmartHub.Domain.Common.Extensions
{
	/// <summary>
	/// Uri /Url extensions.
	/// </summary>
	public static class UriExtension
	{
		/// <summary>
		/// Build a uri from the uriBuilder without the last "/"
		/// </summary>
		/// <param name="uriBuilder">The uriBuilder.</param>
		/// <returns>A uri as string.</returns>
		public static string ToStringWithoutTrailingSlash(this UriBuilder uriBuilder)
		{
			return uriBuilder.ToString().TrimEnd('/');
		}
	}
}