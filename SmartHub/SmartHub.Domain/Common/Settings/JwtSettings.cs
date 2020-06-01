using System;

namespace SmartHub.Domain.Common.Settings
{
	public class JwtSettings
	{
		public string Secret { get; }
		public TimeSpan TokenLifeTime { get; }

		public JwtSettings(string secret, TimeSpan lifeTime)
		{
			Secret = secret;
			TokenLifeTime = lifeTime;
		}
	}
}
