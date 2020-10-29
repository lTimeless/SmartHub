using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.DomainEvents
{
    public sealed class DeviceUpdatedEvent : BaseDomainEvent
    {
        public string DeviceId { get; set; }
        public string State { get; set; }

        public DeviceUpdatedEvent(string state, string deviceId, EventTypes eventType = default)
        {
            EventType = eventType == default ? EventType : eventType.ToString();
            State = state;
            DeviceId = deviceId;
        }
    }
}