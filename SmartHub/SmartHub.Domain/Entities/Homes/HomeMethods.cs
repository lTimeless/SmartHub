using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities.Devices;
using SmartHub.Domain.Entities.Groups;
using SmartHub.Domain.Entities.Plugins;
using SmartHub.Domain.Entities.Settings;
using SmartHub.Domain.Entities.Users;
using SmartHub.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SmartHub.Domain.Entities.Homes
{
	public partial class Home
	{
		public Home AddUser(User user)
		{
			if (Users == null)
			{
				Users = new List<User>() { user };
			}
			else
			{
				Users.Add(user);
			}
			AddDomainEvent(new HomeChangedEvent(Id, null, Users, null, null, null, null, null));
			return this;
		}

		public Home AddGroup(Group group)
		{
			if (Groups is null)
			{
				Groups = new List<Group>() { group };
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
				Settings = new List<Setting>() { setting };
			}
			else
			{
				Settings.Add(setting);
			}

			return this;
		}

		public Home RemoveSetting(Setting setting)
		{
			if (setting.Type == SettingTypeEnum.Default)
			{
				return this;
			}

			if (Settings is null)
			{
				Settings = new List<Setting>() { setting };
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
				AddDomainEvent(new HomeAddPluginEvent(plugin.Name));
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
				Devices = new List<Device>() { device };
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
				if (Devices != null)
				{
					Devices.Remove(device);
				}
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
	}
}
