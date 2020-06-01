using SmartHub.Domain.Common.EventTypes;
using SmartHub.Domain.Entities.Devices;
using SmartHub.Domain.Entities.Groups;
using SmartHub.Domain.Entities.Plugins;
using SmartHub.Domain.Entities.Settings;
using SmartHub.Domain.Entities.Users;
using System.Collections.Generic;

namespace SmartHub.Domain.Entities.Homes
{
	public sealed class HomeChangedEvent : IDomainEvent
	{
		public string HomeId { get; }
		public string? Name { get; }
		public string? Description { get; }
		public List<User>? Users { get; }
		public List<Group>? Groups { get; }

		public List<Plugin>? Plugins { get; }

		public List<Device>? Devices { get; }

		public List<Setting>? Settings { get; }

		public Address? Address { get; set; }

		public HomeChangedEvent(string homeId, string? name, List<User>? users, List<Group>? groups, List<Device>? devices, List<Setting>? settings, List<Plugin>? plugins, Address? address)
		{
			HomeId = homeId;
			Users = users;
			Name = name;
			Groups = groups;
			Plugins = plugins;
			Devices = devices;
			Settings = settings;
			Address = address;
		}
	}
}
