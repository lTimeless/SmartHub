using System;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.DomainEvents
{
	public abstract class DomainEvent : IEvent
	{
		public string EventId { get; }
		public virtual string EventType { get; set; } = EventTypes.Domain.ToString();

		protected DomainEvent()
		{
			EventId = Guid.NewGuid().ToString();
		}
	}
}
