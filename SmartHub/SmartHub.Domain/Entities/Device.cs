using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	public class Device : BaseEntity
	{


		public IpAddress Ip { get; private set; } = default!;
		public Company Company { get; } = default!;
		public ConnectionTypes PrimaryConnection { get; private set; }
		public ConnectionTypes SecondaryConnection { get; private set; }
		public string PluginName { get; } = default!; // Equals the Name Property in the IPlugin
		public PluginTypes PluginTypes { get; }// Equals the PluginType Property in the IPlugin

		private Device()
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

		#region Methods

		public Device SetIp(string ip)
		{
			Ip = new IpAddress(ip);
			return this;
		}

		public Device SetConnectionTypes(ConnectionTypes primary, ConnectionTypes secondary)
		{
			PrimaryConnection = primary;
			SecondaryConnection = secondary;
			return this;
		}



		#endregion
	}
}
