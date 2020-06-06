using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.BasePlugin;
using SmartHub.BasePlugin.Interfaces.DeviceTypes;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	public interface IPluginHostService
	{
		/// <summary>
		/// Loads all Plugins
		/// </summary>
		IPluginLoadService<IPlugin> Plugins { get; }

		/// <summary>
		/// Loads only light plugins
		/// </summary>
		IPluginLoadService<ILight> LightPlugins { get; }

		/// <summary>
		/// Loads only door plugins
		/// </summary>
		IPluginLoadService<IDoor> DoorPlugins { get; }

	}
}
