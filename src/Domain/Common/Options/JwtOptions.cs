using System.ComponentModel.DataAnnotations;

namespace SmartHub.Domain.Common.Options
{
	public class JwtOptions
	{
		[Required] public string Key { get; set; } = default!;
		public double LifeTimeInMinutes { get; set; }
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
	}
}