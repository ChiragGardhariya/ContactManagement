using System;

namespace ContactAPI
{
	public static class CommonFunctionality
	{
		public static DateTime GetISTTime()
		{
			TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

			return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
		}
	}
}