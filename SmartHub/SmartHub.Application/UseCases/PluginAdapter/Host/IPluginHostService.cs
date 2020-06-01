using PluginBase;
using PluginBase.Interfaces.DeviceType;
using SmartHub.Application.UseCases.PluginAdapter.Loader;

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
