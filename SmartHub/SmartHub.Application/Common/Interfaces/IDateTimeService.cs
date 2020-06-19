using System;
using NodaTime;

namespace SmartHub.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        DateTimeZone TimeZone{get;}

        LocalDateTime? OffsetDateTime{get;}

        Instant Now{ get;}
        Instant NowUtc { get; }

        LocalDateTime LocalNow{get;}

        Instant ToInstant(LocalDateTime local);

        LocalDateTime ToLocal(Instant instant);
    }
}
