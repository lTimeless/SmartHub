using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.DomainEvents
{
	public sealed class HomeUpdatedEvent : BaseDomainEvent
	{
		public string? Name { get; }
		public string? Description { get; }
		public User? NewUser { get; }
		public Group? NewGroup { get; }
		public Plugin? NewPlugin { get; }
		public Device? NewDevice { get; }
		public Configuration? NewConfiguration { get; }
		public Address? NewAddress { get;  }

		public HomeUpdatedEvent(string name, string? description)
		{
			Name = name;
			Description = description ?? "";
		}

		public HomeUpdatedEvent(Address address)
		{
			NewAddress = address;
		}

		public HomeUpdatedEvent(User newUser)
		{
			NewUser = newUser;
		}

		public HomeUpdatedEvent(Group newGroup)
		{
			NewGroup = newGroup;
		}

		public HomeUpdatedEvent(Plugin newPlugin)
		{
			NewPlugin = newPlugin;
		}

		public HomeUpdatedEvent(Device newDevice)
		{
			NewDevice = newDevice;
		}

		public HomeUpdatedEvent(Configuration newConfiguration)
		{
			NewConfiguration = newConfiguration;
		}

	}
}
