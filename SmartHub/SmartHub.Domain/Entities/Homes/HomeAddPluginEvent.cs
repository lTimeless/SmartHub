
using SmartHub.Domain.Common.EventTypes;

namespace SmartHub.Domain.Entities.Homes
{
	public class HomeAddPluginEvent : IDomainEvent
	{
		public string PluginName { get; }

		public HomeAddPluginEvent(string name)
		{
			PluginName = name;
		}
	}
}
