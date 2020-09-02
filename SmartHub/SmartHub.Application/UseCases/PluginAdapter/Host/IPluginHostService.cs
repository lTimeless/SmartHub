using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.BasePlugin;
using SmartHub.BasePlugin.Interfaces.DeviceTypes;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	/// <summary>
	/// Service for holding infos about available plugins
	/// Here you can load a specific plugin
	/// </summary>
	public interface IPluginHostService<T> where T: class
	{
		Task<T> GetPluginByName();
		// /// <summary>
		// /// Loads only light plugins
		// /// </summary>
		// IPluginLoadService<ILight> LightPlugins { get; }
		//
		// /// <summary>
		// /// Loads only door plugins
		// /// </summary>
		// IPluginLoadService<IDoor> DoorPlugins { get; }

	}
}
