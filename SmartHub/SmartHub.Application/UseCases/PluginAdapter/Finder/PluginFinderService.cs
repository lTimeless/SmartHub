using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Util;
using SmartHub.Domain.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.PluginAdapter.Finder
{
	public class PluginFinderService : IPluginFinderService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PluginFinderService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// put entire UnloadableAssemblyLoadContext in a method to avoid caller holding on to the reference
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IReadOnlyDictionary<string, FoundPluginDto> FindPluginsInAssemblies(string path)
		{
			var assemblyPluginInfos = new Dictionary<string, FoundPluginDto>();
			(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = GetValidAssembliesAndLoadContext(path);

			foreach (var assembly in assemblies)
			{
				foreach (var plugin in PluginUtils.GetPluginTypes(assembly))
				{
					var pluginDto = new FoundPluginDto(plugin.Name, assembly.Location);
					assemblyPluginInfos.Add(plugin.Name, pluginDto);
				}
			}
			pluginLoadContext.Unload();
			return assemblyPluginInfos;
		}

		public async Task<IReadOnlyDictionary<string, FoundPluginDto>> FilterByPluginsInHome(
			IReadOnlyDictionary<string, FoundPluginDto> foundPluginDtosDictionary)
		{
			var home = await _unitOfWork.HomeRepository.GetFirstAsync();
			var pluginsInHome = home.Plugins;
			if (pluginsInHome.IsNullOrEmpty())
			{
				return foundPluginDtosDictionary;
			}
			var finalDictionary = new Dictionary<string, FoundPluginDto>();
			foreach (var (key, value) in foundPluginDtosDictionary)
			{
				if (pluginsInHome.Any(x => x.Name == key))
				{
					continue;
				}
				finalDictionary.Add(key, value);
			}
			return finalDictionary;
		}

		public Tuple<PluginLoadContext, IEnumerable<Assembly>> GetValidAssembliesAndLoadContext(string path)
		{
			var pluginLoadContext = new PluginLoadContext();
			var assemblies = Directory
				.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories)
				.Select(pluginLoadContext.LoadFromAssemblyPath)
				.Where(x => !PluginUtils.GetPluginTypes(x).IsNullOrEmpty());
			return new Tuple<PluginLoadContext, IEnumerable<Assembly>>(pluginLoadContext, assemblies);
		}

		public Tuple<PluginLoadContext, Assembly> GetValidAssemblyAndLoadContext(string path)
		{
			var pluginLoadContext = new PluginLoadContext();
			var assembly = pluginLoadContext.LoadFromAssemblyPath(path);
			if (assembly is null)
			{
				throw new PluginException($"[{nameof(GetValidAssemblyAndLoadContext)}] Error: Could not load the assembly under the given path: {path}");
			}
			return new Tuple<PluginLoadContext, Assembly>(pluginLoadContext, assembly);
		}

	}
}
