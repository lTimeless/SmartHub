using SmartHub.Domain.Common.EventTypes;
using System.Collections.Generic;

namespace SmartHub.Domain.Entities
{
	public interface IAggregateRoot
	{
		public List<IDomainEvent> Events { get; set; }

		public void AddDomainEvent(IDomainEvent domainEvent);

		public void ClearDomainEvents();
	}
}
