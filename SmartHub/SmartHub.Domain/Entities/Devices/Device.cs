using SmartHub.Domain.Enums;

namespace SmartHub.Domain.Entities.Devices
{
	public class Device : BaseEntity
	{
		public virtual IpAddress Ip { get; private set; }

		public virtual Company Company { get; private set; }

		public ConnectionTypeEnum PrimaryConnection { get; private set; }

		public ConnectionTypeEnum SecondaryConnection { get; private set; }

		public string PluginName { get; } // Equals the Name Property in the IPlugin
		public PluginTypeEnum PluginType { get; }// Equals the PluginType Property in the IPlugin

		protected Device()
		{
		}

		public Device(
			string name,
			string description,
			string ip,
			string manufacturer,
			ConnectionTypeEnum primaryConnection,
			ConnectionTypeEnum secondaryConnection,
			string groupCreator,
			string pluginName,
			PluginTypeEnum? pluginType) :
			base(name, description)
		{
			Ip = new IpAddress(ip);
			Company = new Company(manufacturer);
			PrimaryConnection = primaryConnection;
			SecondaryConnection = secondaryConnection;
			CreatedBy = groupCreator;
			PluginName = pluginName;
			PluginType = pluginType ?? PluginTypeEnum.None;
		}
	}
}
