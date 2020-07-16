using System.Collections.Generic;
using SmartHub.Domain.DomainEvents;

namespace SmartHub.Domain.Entities
{
	public interface IAggregateRoot
	{
		public List<IDomainEvent> Events { get; set; }

		public void AddDomainEvent(IDomainEvent domainEvent);

		public void ClearDomainEvents();
	}
}
