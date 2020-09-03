using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.UseCases.PluginAdapter.Helper;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	public class PluginLoadService: IPluginLoadService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginCreatorService _pluginCreator;

		public PluginLoadService(IUnitOfWork unitOfWork, IPluginCreatorService pluginCreator)
		{
			_unitOfWork = unitOfWork;
			_pluginCreator = pluginCreator;
		}

		/// <inheritdoc cref="IPluginLoadService.LoadByName"/>
		[MethodImpl(MethodImplOptions.NoInlining)] // put entire unloadable AssemblyLoadContext in a method to avoid caller holding on to the reference
		public async Task<IPlugin> LoadByName(string pluginName)
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			if (home is null)
			{
				throw new PluginException("Error: No home created yet.");
			}
			var setting = home.Settings.FirstOrDefault(c => c.IsActive);

			var foundAllFindPluginsInAssembliesDictionary = FindPluginsInAssemblies(setting.PluginPath);
			if (!foundAllFindPluginsInAssembliesDictionary.ContainsKey(pluginName))
			{
				throw new PluginException($"Error: No plugin found on your machine for the given name - {pluginName}");
			}

			var pluginDto = foundAllFindPluginsInAssembliesDictionary[pluginName];
			if (!PluginHelper.ValidatePath(pluginDto.Path))
			{
				throw new PluginException($"Error: Couldn't load plugin {pluginName}");
			}
			(PluginLoadContext pluginLoadContext, Assembly assembly) = LoadAssemblyAndContext(pluginDto.Path);

			var iPluginsFromAssembly = _pluginCreator.CreateIPluginsFromAssembly(assembly);

			if (iPluginsFromAssembly.IsNullOrEmpty())
			{
				throw new PluginException($"Error: While receiving the IPlugin from {nameof(_pluginCreator.CreateIPluginsFromAssembly)}");
			}

			var foundIPlugin = iPluginsFromAssembly.First(x => x.Key.Equals(pluginName));
			pluginLoadContext.Unload();

			return foundIPlugin.Value;
		}

		// put entire UnloadableAssemblyLoadContext in a method to avoid caller holding on to the reference
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IReadOnlyDictionary<string, PluginDto> FindPluginsInAssemblies(string path)
		{
			var assemblyPluginInfos = new Dictionary<string, PluginDto>();
			(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = LoadAssembliesAndContext(path);

			foreach (var assembly in assemblies)
			{
				foreach (var plugin in PluginHelper.GetValidPluginTypes(assembly))
				{
					// plugin.name === class name
					var pluginDto = new PluginDto(plugin.Name, assembly.Location);
					assemblyPluginInfos.Add(plugin.Name, pluginDto);
				}
			}
			pluginLoadContext.Unload();
			return assemblyPluginInfos;
		}




		public async Task<IReadOnlyDictionary<string, PluginDto>> FilterByPluginsInHome(
			IReadOnlyDictionary<string, PluginDto> foundPluginDtosDictionary)
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			if (home.Plugins.IsNullOrEmpty())
			{
				return foundPluginDtosDictionary;
			}
			var finalDictionary = new Dictionary<string, PluginDto>();
			foreach (var (key, value) in foundPluginDtosDictionary)
			{
				if (home.Plugins.Any(x => x.Name == key))
				{
					continue;
				}
				finalDictionary.Add(key, value);
			}
			return finalDictionary;
		}





		private Tuple<PluginLoadContext, IEnumerable<Assembly>> LoadAssembliesAndContext(string path)
		{
			var pluginLoadContext = new PluginLoadContext();
			var assemblies = Directory
				.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories)
				.Select(pluginLoadContext.LoadFromAssemblyPath)
				.Distinct();
			return new Tuple<PluginLoadContext, IEnumerable<Assembly>>(pluginLoadContext, assemblies);
		}

		private Tuple<PluginLoadContext, Assembly> LoadAssemblyAndContext(string path)
		{
			var pluginLoadContext = new PluginLoadContext();
			var assembly = pluginLoadContext.LoadFromAssemblyPath(path);
			if (assembly is null)
			{
				throw new PluginException($"[{nameof(LoadAssemblyAndContext)}] Error: Could not load the assembly under the given path: {path}");
			}
			return new Tuple<PluginLoadContext, Assembly>(pluginLoadContext, assembly);
		}
	}
}
