using SmartHub.BasePlugin;
using System.Collections.Generic;
using System.Reflection;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.PluginAdapter.Creator
{
	/// <summary>
	/// Service for creating Plugins
	/// </summary>
	public interface IPluginCreatorService
	{
		/// <summary>
		///	Creates all IPlugins from the given assembly
		/// </summary>
		/// <param name="assembly">the assembly wit all the iPlugins</param>
		/// <returns>A dictionary with the plugin name as key and the created iPlugin instance as value</returns>
		Dictionary<string, IPlugin> CreateIPluginsFromAssembly(Assembly assembly);

		/// <summary>
		/// Creates a list of Plugin entities from a list of IPlugins
		/// </summary>
		/// <param name="iPluginsList"> that list that is going to be mapped</param>
		/// <param name="assemblyLocation"> the location of the assembly of the system</param>
		/// <returns> a list of plugin entities</returns>
		List<Plugin> CreatePluginsFromIPlugins(IEnumerable<IPlugin> iPluginsList, string assemblyLocation);
	}
}
