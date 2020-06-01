using SmartHub.Domain.Common.EventTypes;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities.Devices;
using SmartHub.Domain.Entities.Groups;
using SmartHub.Domain.Entities.Plugins;
using SmartHub.Domain.Entities.Settings;
using SmartHub.Domain.Entities.Users;
using System.Collections.Generic;

namespace SmartHub.Domain.Entities.Homes
{
	public partial class Home : BaseEntity, IAggregateRoot
	{
		public virtual List<User> Users { get; internal set; }
		public virtual List<Group> Groups { get; internal set; }

		public virtual List<Plugin> Plugins { get; internal set; } // make it so that all plugins will be saved for backup /restore etc.

		public virtual List<Device> Devices { get; internal set; }

		public virtual List<Setting> Settings { get; internal set; }

		public virtual Address Address { get; set; }
		public List<IDomainEvent> Events { get; set; }

		public Home()
		{
		}

		public Home(string name, string description, Setting setting) : base(name, description)
		{
			Users = new List<User>();
			Devices = new List<Device>();
			Groups = new List<Group>();
			Plugins = new List<Plugin>();
			Settings = new List<Setting>() { setting };
			Events = new List<IDomainEvent>();
		}

		public void AddDomainEvent(IDomainEvent domainEvent)
		{
			if (Events.IsNullOrEmpty())
			{
				Events = new List<IDomainEvent>();
			}
			Events.Add(domainEvent);
		}

		public void ClearDomainEvents()
		{
			Events.Clear();
		}
	}
}
