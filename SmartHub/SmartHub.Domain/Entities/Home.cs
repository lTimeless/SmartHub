using System.Collections.Generic;
using System.Linq;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.DomainEvents;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	public class Home : BaseEntity, IAggregateRoot
	{
		public virtual List<User> Users { get; } = new List<User>();
		public virtual  List<Group> Groups { get; } = new List<Group>();
		public virtual  List<Plugin> Plugins { get; } = new List<Plugin>(); // make it so that all plugins will be saved for backup /restore etc.
		public virtual  List<Configuration> Configurations { get; } = new List<Configuration>();
		public virtual  List<Activity> Activities { get; } = new List<Activity>();
		public Address? Address { get; private set; }
		public virtual  List<BaseDomainEvent> Events { get; private set; } = new List<BaseDomainEvent>();

		// Needed for ef core
		protected Home()
		{
		}

		public Home(string name, string description) : base(name, description)
		{
		}

		#region Methods
		public void AddDomainEvent(BaseDomainEvent domainEvent)
		{
			if (Events.IsNullOrEmpty())
			{
				Events = new List<BaseDomainEvent>();
			}
			Events.Add(domainEvent);
		}

		public void ClearDomainEvents()
		{
			Events.Clear();
		}

		public Home AddAddress(string city, string state, string country, string zipCode, string street = "")
		{
			Address = new Address(street, city, state, country, zipCode);
			return this;
		}
		public Home AddUser(User user)
		{
			Users.Add(user);
			AddDomainEvent(new HomeUpdatedEvent( user ));
			return this;
		}
		public Home AddGroup(Group newGroup)
		{
			Groups.Add(newGroup);
			return this;
		}
		public Home AddDevice(Device newDevice, string groupName)
		{
			var group = Groups.Find(x => x.Name == groupName)
			            ?? Groups.SelectMany(x => x.SubGroups)
				            .ToList()
				            .Find(c => c.Name == groupName);
			group?.AddDevice(newDevice);
			AddDomainEvent(new HomeUpdatedEvent(newDevice));
			return this;
		}
		public Home AddSetting(Configuration configuration)
		{
			Configurations.Add(configuration);
			return this;
		}
		public Home AddPlugins(List<Plugin> plugins)
		{
			Plugins.AddRange(plugins);
			foreach (var plugin in plugins)
			{
				AddDomainEvent(new HomeUpdatedEvent(plugin));
			}
			return this;
		}
		public Home AddActivity(Activity newActivity)
		{
			Activities.Add(newActivity);
			return this;
		}

		/// <summary>
		/// Checks if the new plugin doesn't already exists or if it has a higher version than on db
		/// </summary>
		/// <param name="newPlugin">The plugin to check for</param>
		/// <returns>true if exists of the version is higher on the db</returns>
		public bool CheckIfPluginExistAndHasHigherVersion(Plugin newPlugin)
		{
			return Plugins.Exists(x => x.Name == newPlugin.Name && x.AssemblyVersion > newPlugin.AssemblyVersion);
		}

		/// <summary>
		/// Updated a group
		/// </summary>
		/// <param name="id">The ID to find the group</param>
		/// <param name="name">The new name</param>
		/// <param name="description">The new description</param>
		/// <returns></returns>
		public bool UpdateGroup(string id, string? name, string? description)
		{
			var foundGroup = Groups.Find(x => x.Id == id);
			if (foundGroup is null)
			{
				return false;
			}

			if (!string.IsNullOrEmpty(name))
			{
				foundGroup.SetName(name);
			}
			if (!string.IsNullOrEmpty(description))
			{
				foundGroup.SetDescription(description);
			}

			AddDomainEvent(new GroupUpdatedEvent(StateTypes.Modified.ToString(), foundGroup.Id));
			return true;
		}

		public bool UpdateDevice(string id, string? name, string? description, string? ipv4, ConnectionTypes primary, ConnectionTypes secondary)
		{
			var foundDevice = this.FindDevice(id);
			if (foundDevice is null)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(name))
			{
				foundDevice.SetName(name);
			}
			if (!string.IsNullOrEmpty(description))
			{
				foundDevice.SetDescription(description);
			}
			if (!string.IsNullOrEmpty(ipv4))
			{
				foundDevice.SetIp(ipv4);
			}
			foundDevice.SetConnectionTypes(primary, secondary);
			AddDomainEvent(new DeviceUpdatedEvent(StateTypes.Modified.ToString(), foundDevice.Id));
			return true;
		}

		#endregion
	}
}
