using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	public interface IPluginLoadService<T> where T : class
	{
		/// <summary>
		/// Gets and loads an iPlugin by name
		/// </summary>
		/// <param name="pluginName">the plugin name from the iPlugin</param>
		/// <returns>the wanted iPlugin or null</returns>
		Task<T> GetIPluginByName(string pluginName);

		Task<IEnumerable<T>> GetIPluginsByPath(string assemblyPath);


		/// <summary>
		/// Loads IPlugins, from multiple assemblies and creates Plugins if they don't exist yet
		/// </summary>
		/// <param name="assemblyPaths">the assemblies where to load all plugin from</param>
		/// <returns>task completed, returns, or throws en exception if it could not create new plugin entities</returns>
		Task<bool> LoadAndAddIPluginsToHome(IEnumerable<string> assemblyPaths);

		/// <summary>
		/// Loads IPlugins, from the given assembly and creates Plugins if they don't exist yet
		/// </summary>
		/// <param name="assemblyPath"> loads all plugins from one assembly</param>
		/// <returns>task completed, returns, or throws en exception if it could not create a new plugin entity</returns>
		Task<bool> LoadAndAddIPluginToHomeByPath(string assemblyPath);


	}
}
