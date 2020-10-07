using System;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.DomainEvents
{
	public abstract class BaseDomainEvent : IBaseEvent
	{
		public string EventId { get; }
		public virtual string EventType { get; set; } = EventTypes.Domain.ToString();

		protected BaseDomainEvent()
		{
			EventId = Guid.NewGuid().ToString();
		}
	}
}
