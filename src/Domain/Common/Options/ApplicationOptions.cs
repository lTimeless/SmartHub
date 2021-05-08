using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;

namespace SmartHub.Domain.Common.Options
{
	/// <summary>
	///     All options for the application.
	/// </summary>
	public class ApplicationOptions
	{
		// TODO add HostOptions, LogToFile, Use_DevProxy, Persistence
		public ApplicationOptions()
		{
			CacheProfiles = new();
		}

		[Required] public CacheProfileOptions CacheProfiles { get; }

		public CompressionOptions Compression { get; set; } = default!;

		[Required] public ForwardedHeadersOptions ForwardedHeaders { get; set; } = default!;

		[Required] public GraphQlOptions GraphQl { get; set; } = default!;

		[Required] public JwtOptions Jwt { get; set; } = default!;
	}
}