using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.BasePlugin;
using SmartHub.BasePlugin.Interfaces.DeviceTypes;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	public class PluginHostService : IPluginHostService
	{
		public IPluginLoadService<IPlugin> Plugins { get; }
		public IPluginLoadService<ILight> LightPlugins { get; }
		public IPluginLoadService<IDoor> DoorPlugins { get; }

		public PluginHostService(IPluginLoadService<IPlugin> pluginLoader, IPluginLoadService<ILight> lightLoader, IPluginLoadService<IDoor> doorLoader)
		{
			Plugins = pluginLoader;
			LightPlugins = lightLoader;
			DoorPlugins = doorLoader;
		}
	}
}
