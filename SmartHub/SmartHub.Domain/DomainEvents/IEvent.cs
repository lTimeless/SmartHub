
namespace SmartHub.Domain.DomainEvents
{
	public interface IEvent
	{
		public string EventId { get; }
		public string EventType { get; }
	}
}
