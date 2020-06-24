using SmartHub.Domain.Common.EventTypes;
using SmartHub.Domain.Entities.Devices;
using SmartHub.Domain.Entities.Groups;
using SmartHub.Domain.Entities.Plugins;
using SmartHub.Domain.Entities.Settings;
using SmartHub.Domain.Entities.Users;
using System.Collections.Generic;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities.Homes
{
	public sealed class HomeUpdatedEvent : IDomainEvent
	{
		public string? Name { get; }
		public string? Description { get; }
		public User? NewUser { get; }
		public Group? NewGroup { get; }

		public Plugin? NewPlugin { get; }

		public Device? NewDevice { get; }

		public Setting? NewSetting { get; }

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

		public HomeUpdatedEvent(Setting newSetting)
		{
			NewSetting = newSetting;
		}

	}
}
