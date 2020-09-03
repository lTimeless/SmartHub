using System.Threading.Tasks;
using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	/// <summary>
	/// Service for holding infos about available plugins
	/// Here you can load a specific plugin
	/// </summary>
	public interface IPluginHostService
	{
		Task<TP> GetPluginByNameAsync<TP>(string pluginName) where TP : IPlugin;

		/// <summary>
		/// Synchronizes after startup the Dictionary with the plugins in the db
		/// so all Iplugins will be loaded into the dictionary
		/// </summary>
		/// <param name="assemblyPath">the assemblies where to load all plugin from</param>
		/// <param name="multiple"></param>
		/// <returns>task completed, returns, or throws en exception if it could not create new plugin entities</returns>
		Task SynchronizeDictionaryWithDb(string assemblyPath, LoadStrategy multiple);

		/// <summary>
		/// Loads IPlugins from the assembly, than creates PluginEntity and adds it to the home Entity
		/// </summary>
		/// <param name="assemblyPath">the assemblies where to load all plugin from</param>
		/// <param name="multiple"></param>
		/// <returns>task completed, returns, or throws en exception if it could not create new plugin entities</returns>
		Task AddToHome(string assemblyPath, LoadStrategy multiple);
	}
}
