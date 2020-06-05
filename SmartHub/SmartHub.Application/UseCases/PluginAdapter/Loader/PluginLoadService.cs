using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	public class PluginLoadService<T> : IPluginLoadService<T> where T : class
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginCreatorService<T> _pluginCreator;
		private readonly IPluginFinderService _pluginFinderService;

		public PluginLoadService(IUnitOfWork unitOfWork, IPluginCreatorService<T> pluginCreator, IPluginFinderService pluginFinderService)
		{
			_unitOfWork = unitOfWork;
			_pluginCreator = pluginCreator;
			_pluginFinderService = pluginFinderService;
		}

		/// <inheritdoc cref="IPluginLoadService.GetIPluginByName"/>
		public async Task<T> GetIPluginByName(string pluginName)
		{
			if (string.IsNullOrEmpty(pluginName))
			{
				throw new SmartHubException($"[{nameof(GetIPluginByName)}] The given pluginName is null");
			}

			var home = await _unitOfWork.HomeRepository.GetFirstAsync();
			var plugin = home.Plugins.Find(x => x.Name == pluginName);
			if (plugin is null)
			{
				throw new SmartHubException($"[{nameof(GetIPluginByName)}] No plugin found under the given name : {pluginName}");
			}

			(PluginLoadContext pluginLoadContext, Assembly assembly) = LoadOne(plugin.AssemblyFilepath);

			var dictionaryOfIPlugin = _pluginCreator.CreateIPluginsFromAssembly(assembly);

			pluginLoadContext.Unload();
			if (dictionaryOfIPlugin.IsNullOrEmpty())
			{
				throw new PluginException($"[{nameof(GetIPluginByName)}] Error: While receiving the IPlugin from {nameof(_pluginCreator.CreateIPluginsFromAssembly)}");
			}

			var foundIPlugin = dictionaryOfIPlugin.First(x => x.Key.Equals(plugin.Name));
			return (T)foundIPlugin.Value;
		}

		/// <inheritdoc cref="IPluginLoadService.GetIPluginsByPath"/>
		public async Task<IEnumerable<T>> GetIPluginsByPath(string assemblyPath)
		{
			if (string.IsNullOrEmpty(assemblyPath))
			{
				throw new SmartHubException($"[{nameof(GetIPluginsByPath)}] The given assemblyPath is null");
			}

			(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = LoadMultiple(assemblyPath);
			var listOfIPlugins = new List<T>();
			foreach (var assembly in assemblies)
			{
				var dictionaryOfIPlugin = _pluginCreator.CreateIPluginsFromAssembly(assembly);
				listOfIPlugins.AddRange(dictionaryOfIPlugin.Values);
			}
			pluginLoadContext.Unload();
			return listOfIPlugins;
		}



		/// <inheritdoc cref="IPluginLoadService.LoadAndAddIPluginToHomeByPath"/>
		public async Task<bool> LoadAndAddIPluginToHomeByPath(string assemblyPath)
		{
			if (string.IsNullOrEmpty(assemblyPath))
			{
				return false;
			}
			(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = LoadMultiple(assemblyPath);
			foreach (var assembly in assemblies)
			{
				await AddToHome(_pluginCreator.CreateIPluginsFromAssembly(assembly), assembly);
			}
			pluginLoadContext.Unload();
			return true;
		}

		/// <inheritdoc cref="IPluginLoadService.LoadAndAddIPluginsToHome"/>
		public async Task<bool> LoadAndAddIPluginsToHome(IEnumerable<string> assemblyPaths)
		{
			if (assemblyPaths.IsNullOrEmpty())
			{
				return false;
			}
			foreach (var path in assemblyPaths)
			{
				(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = LoadMultiple(path);
				foreach (var assembly in assemblies)
				{
					await AddToHome(_pluginCreator.CreateIPluginsFromAssembly(assembly), assembly);
				}
				pluginLoadContext.Unload();
			}
			return true;
		}



		// put entire UnloadableAssemblyLoadContext in a method to avoid caller holding on to the reference
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Tuple<PluginLoadContext, IEnumerable<Assembly>> LoadMultiple(string path)
		{
			if (!Directory.Exists(path))
			{
				throw new PluginException($"[PluginHostService.{nameof(LoadMultiple)}] The given path does not exist, path: {path}");
			}
			return _pluginFinderService.GetValidAssembliesAndLoadContext(path);
		}

		// put entire UnloadableAssemblyLoadContext in a method to avoid caller holding on to the reference
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Tuple<PluginLoadContext, Assembly> LoadOne(string path)
		{
			if (!File.Exists(path))
			{
				throw new PluginException($"[PluginHostService.{nameof(LoadOne)}] The given path does not exist, path: {path}");
			}
			return _pluginFinderService.GetValidAssemblyAndLoadContext(path);
		}



		private async Task AddToHome(Dictionary<string, T> iPluginDictionary, Assembly assembly)
		{
			foreach (var (name, iPlugin) in iPluginDictionary)
			{
				var listOfIPlugins = iPluginDictionary.Values.ToList() as IEnumerable<IPlugin>
									 ?? throw new PluginException(
										 $"[{nameof(AddToHome)}] Error converting list of {nameof(iPlugin).GetType()} to list of IPlugin");
				var listOfPlugins = _pluginCreator.CreatePluginsFromIPlugins(listOfIPlugins, assembly.Location);
				var home = await _unitOfWork
					.HomeRepository
					.GetFirstAsync().ConfigureAwait(false);

				foreach (var plugin in listOfPlugins.Where(plugin => home.CheckIfPluginExistAndHasHigherVersion(plugin)))
				{
					listOfPlugins.Remove(plugin);
				}

				home.AddPlugins(listOfPlugins);
				await _unitOfWork.SaveAsync();
			}
		}
	}
}
