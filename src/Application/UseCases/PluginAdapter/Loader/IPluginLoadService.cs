using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHub.BasePlugin;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	/// <summary>
	/// Service for loading and searching of IPlugins
	/// </summary>
	public interface IPluginLoadService
	{
		/// <summary>
		/// Loads an iPlugin by name
		/// </summary>
		/// <param name="pluginName">the plugin name from the iPlugin</param>
		/// <param name="pluginPath">the home plugin path</param>
		/// <returns>the wanted iPlugin or throws Exception</returns>
		Task<IPlugin> LoadByName(string pluginName, string pluginPath);

		/// <summary>
		/// Loads and Creates iPlugins from path
		/// </summary>
		/// <param name="pluginPath">The path where to look for plugins</param>
		/// <returns>Dictionary with pluginName and IPlugin</returns>
		Task<Dictionary<string, IPlugin>> LoadAndCreateIPlugins(string pluginPath);

		/// <summary>
		/// Find available iPlugins in all assemblies
		/// </summary>
		/// <param name="path">The path where to look for plugins</param>
		/// <returns>Dictionary with pluginName and pluginDto</returns>
		IReadOnlyDictionary<string, PluginDto> FindPluginsInAssemblies(string path);
	}
}
