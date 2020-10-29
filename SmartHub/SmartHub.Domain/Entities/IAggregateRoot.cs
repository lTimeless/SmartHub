using System.Collections.Generic;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Domain.Entities
{
	public interface IAggregateRoot
	{
		public List<BaseDomainEvent> Events { get; set; }

		public void AddDomainEvent(BaseDomainEvent domainEvent);

		public void ClearDomainEvents();
	}
}
