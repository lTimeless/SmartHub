using System.Threading.Tasks;
using SmartHub.BasePlugin;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	/// <summary>
	/// Service for loading IPlugins
	/// </summary>
	public interface IPluginLoadService
	{
		/// <summary>
		/// Gets and loads an iPlugin by name
		/// </summary>
		/// <param name="pluginName">the plugin name from the iPlugin</param>
		/// <returns>the wanted iPlugin or throws Exception</returns>
		Task<IPlugin> LoadByName(string pluginName);
	}
}
