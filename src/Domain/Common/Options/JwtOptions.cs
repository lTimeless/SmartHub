namespace SmartHub.Domain.Common.Options
{
	public class JwtOptions
	{
		public string? Key { get; set; }
		public double LifeTimeInMinutes { get; set; }
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
	}
}
