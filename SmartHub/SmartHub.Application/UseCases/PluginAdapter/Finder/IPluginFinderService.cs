using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.PluginAdapter.Finder
{
	/// <summary>
	/// Service for finding Plugins
	/// </summary>
	public interface IPluginFinderService
	{
		/// <summary>
		/// Finds plugins in the given assembly
		/// </summary>
		/// <param name="path">Path to assembly</param>
		/// <returns>Dictionay with th eplugins name an an dto with more infos</returns>
		IReadOnlyDictionary<string, FoundPluginDto> FindPluginsInAssemblies(string path);

		/// <summary>
		/// Filters the given dictionary by the plugins that already exist in smartHub
		/// </summary>
		/// <param name="foundPluginDtosDictionary">The dictionary to filter</param>
		/// <returns>The filtered dictionary</returns>
		Task<IReadOnlyDictionary<string, FoundPluginDto>> FilterByPluginsInHome(IReadOnlyDictionary<string, FoundPluginDto> foundPluginDtosDictionary);

		/// <summary>
		/// Gets all assemblies and a new created loadContext
		/// </summary>
		/// <param name="path">Path to the assemblies</param>
		/// <returns>A Tuple with the loadContext and IEnumerable with the found assemblies</returns>
		Tuple<PluginLoadContext, IEnumerable<Assembly>> GetAssembliesAndLoadContext(string path);

		/// <summary>
		/// Gets an assembly and a new created loadContext
		/// </summary>
		/// <param name="path">Path to the assembly</param>
		/// <returns>A Tuple with the loadContext and the found assembly</returns>
		Tuple<PluginLoadContext, Assembly> GetAssemblyAndLoadContext(string path);
	}
}
