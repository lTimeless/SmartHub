using System;
using NodaTime;

namespace SmartHub.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        static DateTimeZone TimeZone { get; }

        Instant Now { get; }
        Instant NowUtc { get; }

        LocalDateTime LocalNow{get;}

        Instant ToInstant(LocalDateTime local);

        LocalDateTime ToLocal(Instant instant);
    }
}
