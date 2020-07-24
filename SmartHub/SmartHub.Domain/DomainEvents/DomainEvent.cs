using System;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Domain.DomainEvents
{
	public abstract class DomainEvent : IEvent
	{
		public string Id { get; }
		public virtual string EventType => EventTypes.Domain.ToString();

		protected DomainEvent()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
