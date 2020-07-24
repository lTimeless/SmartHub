
namespace SmartHub.Domain.DomainEvents
{
	public interface IEvent
	{
		public string Id { get; }
		public string EventType { get; }
	}
}
