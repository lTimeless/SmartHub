using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.Identity
{
    public class IdentityEvent : ApplicationEvent
    {
        public string UserName { get; }
        public string State { get; }
        public override string EventType { get; }

        public IdentityEvent(string userName, bool successful, EventTypes eventType = default)
        {
            EventType = eventType == default ? base.EventType : eventType.ToString();
            UserName = userName;
            State = successful ? "Successful" : "Failed";
        }
    }
}