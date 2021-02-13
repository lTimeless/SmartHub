using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	public class Plugin : BaseEntity
	{
		public PluginTypes PluginTypes { get; private set; }
		public string AssemblyFilepath { get; private set; }
		public bool Active { get; private set; }
		public string AssemblyVersion { get; private set; }
		public Company Company { get; private set; }
		public ConnectionTypes ConnectionTypes { get; private set; }
		public bool IsDownloaded { get; private set; }

		protected Plugin()
		{
		}

		public Plugin(string name, string description, PluginTypes pluginTypes, string systemPath, bool active, string version, string company, ConnectionTypes connectionTypes) :
			 base(name, description)
		{
			PluginTypes = pluginTypes;
			AssemblyFilepath = systemPath;
			Active = active;
			AssemblyVersion = version;
			Company = new Company(company);
			IsDownloaded = true;
			ConnectionTypes = connectionTypes;
		}
	}
}
