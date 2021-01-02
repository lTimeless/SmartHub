using System.Collections.Generic;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	public class Device : BaseEntity
	{
		public IpAddress Ip { get; set; }
		public Company Company { get; set; }
		public ConnectionTypes PrimaryConnection { get; set; }
		public ConnectionTypes SecondaryConnection { get; set; }
		public string PluginName { get; set; } // Equals the Name Property in the IPlugin
		public PluginTypes PluginTypes { get; set; }// Equals the PluginType Property in the IPlugin

		public virtual List<Group> Groups { get; set; } = new();

		public Device()
		{
		}

		public Device(
			string name,
			string? description,
			string ip,
			string company,
			ConnectionTypes primaryConnection,
			ConnectionTypes? secondaryConnection,
			string pluginName,
			PluginTypes? pluginType) :
			base(name, description)
		{
			Ip = new IpAddress(ip);
			Company = new Company(company);
			PrimaryConnection = primaryConnection;
			SecondaryConnection = secondaryConnection ?? ConnectionTypes.None;
			PluginName = pluginName;
			PluginTypes = pluginType ?? PluginTypes.None;
		}
	}
}
