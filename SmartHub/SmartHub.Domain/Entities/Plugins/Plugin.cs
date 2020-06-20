using SmartHub.Domain.Entities.Devices;
using SmartHub.Domain.Enums;

namespace SmartHub.Domain.Entities.Plugins
{
	public class Plugin : BaseEntity
	{
		public PluginTypeEnum PluginType { get; private set; }
		public string AssemblyFilepath { get; private set; }
		public bool Active { get; private set; }
		public double AssemblyVersion { get; private set; }
		public virtual Company Company { get; private set; }

		// TODO: will be replaced later for an Enum flag "ConnectionTypes"
		public ConnectionTypeEnum ConnectionTypeEnum { get; private set; }
		public bool IsDownloaded { get; private set; }

		public Plugin()
		{
		}

		public Plugin(string name, string description, PluginTypeEnum pluginType, string systemPath, bool active, double version, string company, ConnectionTypeEnum connectionTypeEnum) :
			 base(name, description)
		{
			PluginType = pluginType;
			AssemblyFilepath = systemPath;
			Active = active;
			AssemblyVersion = version;
			Company = new Company(company);
			IsDownloaded = true;
			ConnectionTypeEnum = connectionTypeEnum;
		}

		public void UpdatePlugin(PluginTypeEnum pluginType, string systemPath, bool active, double version, string company, bool isDownloaded, ConnectionTypeEnum connectionTypeEnum)
		{
			PluginType = pluginType;
			AssemblyFilepath = systemPath;
			Active = active;
			AssemblyVersion = version;
			Company = new Company(company);
			IsDownloaded = isDownloaded;
			ConnectionTypeEnum = connectionTypeEnum;
		}
	}
}
