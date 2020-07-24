using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.Identity.Registration
{
    public class RegistrationEvent : ApplicationEvent
    {
        public string UserName { get; }
        public string Successful { get; }
        public override string EventType { get; }

        public RegistrationEvent(string userName, bool successful)
        {
            EventType = EventTypes.Registration.ToString();
            UserName = userName;
            Successful = successful ? "Successful" : "Failed";
        }


    }
}