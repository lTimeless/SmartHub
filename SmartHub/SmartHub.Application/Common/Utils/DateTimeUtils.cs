using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;

namespace SmartHub.Application.Common.Utils
{
    /// <summary>
    /// Utilities for dateTime
    /// </summary>
    public static class DateTimeUtils
    {
        /// <summary>
        /// Returns the timeZone from the System
        /// </summary>
        public static DateTimeZone TimeZone => DateTimeZoneProviders.Tzdb.GetSystemDefault();

        /// <summary>
        /// Return the Current Time as an Instant
        /// </summary>
        public static Instant Now => SystemClock.Instance.GetCurrentInstant();

        /// <summary>
        /// Return the Current Time in UTC as an Instant
        /// </summary>
        public static Instant NowUtc => SystemClock.Instance.InUtc().GetCurrentInstant();

        public static LocalDateTime LocalNow => Now.InZone(TimeZone).LocalDateTime;

        /// <summary>
        /// Generates an instant from the given LocalDateTime
        /// </summary>
        /// <param name="local">The LocalDateTime</param>
        /// <returns>The Instant for the LocalDateTime</returns>
        public static Instant ToInstant(LocalDateTime local)
        {
            return local.InZone(TimeZone, Resolvers.LenientResolver).ToInstant();
        }

        /// <summary>
        /// Generates an LocalDateTime from the given Instans
        /// </summary>
        /// <param name="instant">The Instant</param>
        /// <returns>The LocalDateTime for the Instant</returns>
        public static LocalDateTime ToLocal(Instant instant)
        {
            return instant.InZone(TimeZone).LocalDateTime;
        }
    }
}