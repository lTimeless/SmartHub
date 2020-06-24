using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Infrastructure.Services.DateTime
{
    public class DateTimeService : IDateTimeService
    {
        public static DateTimeZone TimeZone => DateTimeZoneProviders.Tzdb.GetSystemDefault();

        public Instant Now => SystemClock.Instance.GetCurrentInstant();
        public Instant NowUtc => SystemClock.Instance.InUtc().GetCurrentInstant();

        public LocalDateTime LocalNow => Now.InZone(TimeZone).LocalDateTime;

        public Instant ToInstant(LocalDateTime local)
        {
            return local.InZone(TimeZone, Resolvers.LenientResolver).ToInstant();
        }

        public LocalDateTime ToLocal(Instant instant)
        {
            return instant.InZone(TimeZone).LocalDateTime;
        }
    }
}