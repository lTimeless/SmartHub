namespace SmartHub.Domain.Common.Settings
{
	public class JwtSettings
	{
		public string? Key { get; set; }
		public double LifeTimeInMinutes { get; set; }
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
	}
}
