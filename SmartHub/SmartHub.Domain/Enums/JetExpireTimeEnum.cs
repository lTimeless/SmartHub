using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.Domain.Enums
{
	public enum JwtExpireTimeEnum
	{
		HoursToExpire = 2,
		DaysToExpire = 7,
		MonthToExpire = 6,
	}

	public static class JwtExpireTimeEnumExtension
	{
		public static int GetValue(this JwtExpireTimeEnum value)
		{
			return (int)value;
		}
	}
}
