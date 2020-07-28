using System.Collections.Generic;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Domain.Entities
{
	public interface IAggregateRoot
	{
		public List<DomainEvent> Events { get; set; }

		public void AddDomainEvent(DomainEvent domainEvent);

		public void ClearDomainEvents();
	}
}
