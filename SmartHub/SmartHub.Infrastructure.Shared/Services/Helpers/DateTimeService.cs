using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Infrastructure.Shared.Services.Helpers
{
    /// <inheritdoc cref="IDateTimeService"/>
    public class DateTimeService : IDateTimeService
    {
        /// <inheritdoc cref="IDateTimeService.TimeZone"/>
        public DateTimeZone TimeZone => DateTimeZoneProviders.Tzdb.GetSystemDefault();

        /// <inheritdoc cref="IDateTimeService.Now"/>
        public Instant Now => SystemClock.Instance.GetCurrentInstant();

        /// <inheritdoc cref="IDateTimeService.NowUtc"/>
        public Instant NowUtc => SystemClock.Instance.InUtc().GetCurrentInstant();

        /// <inheritdoc cref="IDateTimeService.LocalNow"/>
        public LocalDateTime LocalNow => Now.InZone(TimeZone).LocalDateTime;

        /// <inheritdoc cref="IDateTimeService.ToInstant"/>
        public Instant ToInstant(LocalDateTime local)
        {
            return local.InZone(TimeZone, Resolvers.LenientResolver).ToInstant();
        }

        /// <inheritdoc cref="IDateTimeService.ToLocal"/>
        public LocalDateTime ToLocal(Instant instant)
        {
            return instant.InZone(TimeZone).LocalDateTime;
        }
    }
}