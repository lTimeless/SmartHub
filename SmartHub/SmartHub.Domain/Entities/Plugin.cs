using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	public class Plugin : BaseEntity
	{
		public PluginTypes PluginTypes { get; }
		public string AssemblyFilepath { get; } = default!;
		public bool Active { get; }
		public double AssemblyVersion { get; }
		public Company Company { get; } = default!;
		public ConnectionTypes ConnectionTypes { get; }
		public bool IsDownloaded { get; }

		// Needed for ef core
		protected Plugin()
		{
		}

		public Plugin(string name, string description, PluginTypes pluginTypes, string systemPath, bool active, double version, string company, ConnectionTypes connectionTypes) :
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
