using NodaTime;
using NodaTime.TimeZones;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Infrastructure.Services.DateTime
{
    public class DateTimeService : IDateTimeService
    {

        public DateTimeZone TimeZone => DateTimeZoneProviders.Tzdb.GetSystemDefault();
        public LocalDateTime? OffsetDateTime { get; }

        public Instant Now => OffsetDateTime?.InZoneLeniently(TimeZone).ToInstant() ?? SystemClock.Instance.GetCurrentInstant();

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