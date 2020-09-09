using NodaTime;

namespace SmartHub.Application.Common.Interfaces
{
    /// <summary>
    /// Methods for dateTime
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        /// Returns the timeZone from the System
        /// </summary>
        DateTimeZone TimeZone { get; }
        /// <summary>
        /// Return the Current Time as an Instant
        /// </summary>
        Instant Now { get; }
        /// <summary>
        /// Return the Current Time in UTC as an Instant
        /// </summary>
        Instant NowUtc { get; }
        LocalDateTime LocalNow { get; }
        /// <summary>
        /// Generates an instant from the given LocalDateTime
        /// </summary>
        /// <param name="local">The LocalDateTime</param>
        /// <returns>The Instant for the LocalDateTime</returns>
        Instant ToInstant(LocalDateTime local);
        /// <summary>
        /// Generates an LocalDateTime from the given Instans
        /// </summary>
        /// <param name="instant">The Instant</param>
        /// <returns>The LocalDateTime for the Instant</returns>
        LocalDateTime ToLocal(Instant instant);
    }
}