using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Enums;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	public interface IPluginLoadService<T> where T : class
	{
		/// <summary>
		/// Gets and loads an iPlugin by name
		/// </summary>
		/// <param name="pluginName">the plugin name from the iPlugin</param>
		/// <param name="home">the home</param>
		/// <returns>the wanted iPlugin or null</returns>
		Task<T> GetAndLoadByName(string pluginName, Home home);

		/// <summary>
		/// Gets and loads IPlugins by path
		/// </summary>
		/// <param name="assemblyPath"></param>
		/// <returns>the wanted iPlugins</returns>
		Task<IEnumerable<T>> GetAndLoadByPath(string assemblyPath);


		/// <summary>
		/// Loads IPlugins, from multiple assemblies and creates Plugins if they don't exist yet
		/// </summary>
		/// <param name="assemblyPaths">the assemblies where to load all plugin from</param>
		/// <param name="multiple"></param>
		/// <returns>task completed, returns, or throws en exception if it could not create new plugin entities</returns>
		Task<bool> LoadAndAddToHomeAsync(IEnumerable<string> assemblyPaths, LoadStrategyEnum multiple);

	}
}
