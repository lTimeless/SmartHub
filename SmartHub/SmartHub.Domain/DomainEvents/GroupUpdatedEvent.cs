using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.DomainEvents
{
    public sealed class GroupUpdatedEvent : DomainEvent
    {
        public string GroupId { get; set; }
        public string State { get; set; }

        public GroupUpdatedEvent(string state, string groupId, EventTypes eventType = default)
        {
            EventType = eventType == default ? EventType : eventType.ToString();
            State = state;
            GroupId = groupId;
        }
    }
}