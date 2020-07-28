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

		public virtual List<Device> Devices { get; protected set; }

		public virtual List<Setting> Settings { get; protected set; }

		public virtual Address Address { get; }
		public virtual List<DomainEvent> Events { get; set; }

		protected Home()
		{
		}

		public Home(string name, string description, Setting setting) : base(name, description)
		{
			Users = new List<User>();
			Devices = new List<Device>();
			Groups = new List<Group>();
			Plugins = new List<Plugin>();
			Settings = new List<Setting>() { setting };
			Events = new List<DomainEvent>();
			Address = new Address("","","","",""); // TODO: add functionality
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

		public Home AddUser(User user)
		{
			if (Users == null)
			{
				Users = new List<User> { user };
			}
			else
			{
				Users.Add(user);
			}
			AddDomainEvent(new HomeUpdatedEvent( user ));
			return this;
		}

		public Home AddGroup(Group group)
		{
			if (Groups is null)
			{
				Groups = new List<Group> { group };
			}
			else
			{
				Groups.Add(group);
			}
			return this;
		}

		public Home AddSetting(Setting setting)
		{
			if (Settings is null)
			{
				Settings = new List<Setting> { setting };
			}
			else
			{
				Settings.Add(setting);
			}

			return this;
		}

		public Home RemoveSetting(Setting setting)
		{
			if (setting.Type == SettingTypes.Default)
			{
				return this;
			}

			if (Settings is null)
			{
				Settings = new List<Setting> { setting };
			}
			else
			{
				Settings.Add(setting);
			}
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

		public bool CheckIfPluginExistAndHasHigherVersion(Plugin newPlugin)
		{
			return Plugins.Exists(x => x.Name == newPlugin.Name && x.AssemblyVersion > newPlugin.AssemblyVersion);
		}

		public Home AddDevice(Device device)
		{
			if (Devices == null)
			{
				Devices = new List<Device> { device };
			}
			else
			{
				Devices.Add(device);
			}
			return this;
		}

		public Home RemoveDevice(Device device, string deviceName)
		{
			if (device != null)
			{
				Devices?.Remove(device);
			}
			else if (string.IsNullOrEmpty(deviceName))
			{
				var found = Devices.Single(x => x.Name == deviceName);
				if (Devices != null)
				{
					Devices.Remove(found);
				}
			}
			return this;
		}

		public Home UpdateHome(string? name, string? description, Setting? setting, User? user )
		{
			if (name != null)
			{
				Name = name;
			}
			if (description != null)
			{
				Description = description;
			}
			if (setting != null)
			{
				AddSetting(setting);
			}
			if (user != null)
			{
				AddUser(user);
			}
			return this;
		}
		#endregion

	}
}
