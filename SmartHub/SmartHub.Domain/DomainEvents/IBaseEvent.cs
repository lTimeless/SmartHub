
namespace SmartHub.Domain.DomainEvents
{
	public interface IBaseEvent
	{
		public string EventId { get; }
		public string EventType { get; }
	}
}
