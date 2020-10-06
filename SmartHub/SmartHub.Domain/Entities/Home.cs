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
		public virtual List<User> Users { get; protected set; }
		public virtual List<Group> Groups { get; protected set; }

		public virtual List<Plugin> Plugins { get; protected set; } // make it so that all plugins will be saved for backup /restore etc.

		public virtual List<Setting> Settings { get; protected set; }

		public virtual Address Address { get; private set; }
		public virtual List<DomainEvent> Events { get; set; }

		protected Home()
		{
		}

		public Home(string name, string description) : base(name, description)
		{
			Users = new List<User>();
			Groups = new List<Group>();
			Plugins = new List<Plugin>();
			Events = new List<DomainEvent>();
			Settings = new List<Setting>();
		}

		#region Methods
		public void AddDomainEvent(DomainEvent domainEvent)
		{
			if (Events.IsNullOrEmpty())
			{
				Events = new List<DomainEvent>();
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
		public Home AddDevice(Device newDevice, string groupId)
		{
			var group = Groups.Find(x => x.Id == groupId);
			group?.AddDevice(newDevice);
			AddDomainEvent(new HomeUpdatedEvent(newDevice));
			return this;
		}
		public Home AddSetting(Setting setting)
		{
			Settings.Add(setting);
			return this;
		}

		public Home RemoveSetting(Setting setting)
		{
			if (setting.Type == SettingTypes.Default)
			{
				return this;
			}

			Settings.Add(setting);
			return this;
		}

		public Home AddPlugins(List<Plugin> plugins)
		{
			if (Plugins.IsNullOrEmpty())
			{
				Plugins = new List<Plugin>();
			}

			Plugins.AddRange(plugins);

			foreach (var plugin in plugins)
			{
				AddDomainEvent(new HomeUpdatedEvent(plugin));
			}
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
				foundGroup.UpdateName(name);
			}
			if (!string.IsNullOrEmpty(description))
			{
				foundGroup.UpdateDescription(description);
			}

			AddDomainEvent(new GroupUpdatedEvent(StateTypes.Modified.ToString(), foundGroup.Id));
			return true;
		}
		#endregion
	}
}
