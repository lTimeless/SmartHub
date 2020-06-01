using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.PluginAdapter.Finder
{
	public interface IPluginFinderService
	{
		IReadOnlyDictionary<string, FoundPluginDto> FindPluginsInAssemblies(string path);

		Task<IReadOnlyDictionary<string, FoundPluginDto>> FilterByPluginsInHome(
			IReadOnlyDictionary<string, FoundPluginDto> foundPluginDtosDictionary);

		Tuple<PluginLoadContext, IEnumerable<Assembly>> GetValidAssembliesAndLoadContext(string path);
		Tuple<PluginLoadContext, Assembly> GetValidAssemblyAndLoadContext(string path);
	}
}
