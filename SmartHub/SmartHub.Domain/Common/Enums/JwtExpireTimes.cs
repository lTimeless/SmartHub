namespace SmartHub.Domain.Common.Enums
{
	public enum JwtExpireTime
	{
		HoursToExpire = 2,
		DaysToExpire = 7,
		MonthToExpire = 6
	}

	public static class JwtExpireTimeEnumExtension
	{
		public static int GetValue(this JwtExpireTime value)
		{
			return (int)value;
		}
	}
}
