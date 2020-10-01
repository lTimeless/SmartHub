using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities.Devices;

namespace SmartHub.Domain.Entities
{
	public class Device : BaseEntity
	{
		public virtual IpAddress Ip { get; private set; }

		public virtual Company Company { get; private set; }

		public ConnectionTypes PrimaryConnection { get; private set; }

		public ConnectionTypes SecondaryConnection { get; private set; }

		public string PluginName { get; } // Equals the Name Property in the IPlugin
		public PluginTypes PluginTypes { get; }// Equals the PluginType Property in the IPlugin

		protected Device()
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
