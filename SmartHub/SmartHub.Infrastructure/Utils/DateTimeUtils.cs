using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;

namespace SmartHub.Infrastructure.Utils
{
    public static class DateTimeUtils
    {
        public static DateTimeZone TimeZone => DateTimeZoneProviders.Tzdb.GetSystemDefault();
        public static Instant Now => SystemClock.Instance.GetCurrentInstant();
        public static Instant NowUtc => SystemClock.Instance.InUtc().GetCurrentInstant();

        public static LocalDateTime LocalNow => Now.InZone(TimeZone).LocalDateTime;

        public static Instant ToInstant(LocalDateTime local)
        {
            return local.InZone(TimeZone, Resolvers.LenientResolver).ToInstant();
        }

        public static LocalDateTime ToLocal(Instant instant)
        {
            return instant.InZone(TimeZone).LocalDateTime;
        }
    }
}