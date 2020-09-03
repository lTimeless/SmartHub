using SmartHub.Application.Common.Exceptions;
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
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	public class PluginLoadService<T> : IPluginLoadService<T> where T : class
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginCreatorService<T> _pluginCreator;
		private readonly IPluginFinderService _pluginFinderService;

		// PluginHost sollte die dictionarys verwalten??? damit der Loader nur ladet!!
		//TODO: damit hier nicht jedesmal die assembly neu geladen wird
		// ein dictionary(zeile 51 bereits da alle instances) von typ T und dann bei der Getfunktion wird erst darin anhand des params "name" geprüft
		// und erst danach dann die assembly geladen falls in der liste kein eintrag ist
		// neue einträge überschreiben zu bestehenden key überschreiben diese
		public PluginLoadService(IUnitOfWork unitOfWork, IPluginCreatorService<T> pluginCreator, IPluginFinderService pluginFinderService)
		{
			_unitOfWork = unitOfWork;
			_pluginCreator = pluginCreator;
			_pluginFinderService = pluginFinderService;
		}

		/// <inheritdoc cref="IPluginLoadService{T}.GetAndLoadByName"/>
		public Task<T> GetAndLoadByName(string pluginName, Home home)
		{
			if (string.IsNullOrEmpty(pluginName))
			{
				throw new PluginException($"[{nameof(GetAndLoadByName)}] Error: The given pluginName is null or empty");
			}

			var plugin = home.Plugins.Find(x => x.Name == pluginName);
			if (plugin is null)
			{
				throw new PluginException($"[{nameof(GetAndLoadByName)}] Error: No plugin found, in the database, under the given name : {pluginName}");
			}

			(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assembly) = Load(plugin.AssemblyFilepath, LoadStrategy.Single);

			var dictionaryOfIPlugin = _pluginCreator.CreateIPluginsFromAssembly(assembly.FirstOrDefault());

			pluginLoadContext.Unload();
			if (dictionaryOfIPlugin.IsNullOrEmpty())
			{
				throw new PluginException($"[{nameof(GetAndLoadByName)}] Error: While receiving the IPlugin from {nameof(_pluginCreator.CreateIPluginsFromAssembly)}");
			}

			var foundIPlugin = dictionaryOfIPlugin.First(x => x.Key.Equals(plugin.Name));
			return Task.FromResult(foundIPlugin.Value);
		}
		// TODO: eigentlich brauche ich nur LoadbyName
		/// <inheritdoc cref="IPluginLoadService{T}.GetAndLoadByPath"/>
		public Task<IEnumerable<T>> GetAndLoadByPath(string assemblyPath)
		{
			if (string.IsNullOrEmpty(assemblyPath))
			{
				throw new SmartHubException($"[{nameof(GetAndLoadByPath)}] The given assemblyPath is null");
			}

			(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = Load(assemblyPath, LoadStrategy.Multiple);
			var listOfIPlugins = new List<T>();
			foreach (var assembly in assemblies)
			{
				var dictionaryOfIPlugin = _pluginCreator.CreateIPluginsFromAssembly(assembly);
				listOfIPlugins.AddRange(dictionaryOfIPlugin.Values);
			}
			pluginLoadContext.Unload();
			return Task.FromResult<IEnumerable<T>>(listOfIPlugins);
		}

		// TODO: wird wegfallen, da alle welche nicht im dictionary sind und vom User angefragt werden, geladen werden.
		// und die AddTOHome function wird in neue Funktion "SynchronizeWithDb" umwandern
		/// <inheritdoc cref="IPluginLoadService{T}.LoadAndAddToHomeAsync"/>
		public async Task<bool> LoadAndAddToHomeAsync(IEnumerable<string> assemblyPaths, LoadStrategy multiple)
		{
			var paths = assemblyPaths.ToList();
			if (paths.IsNullOrEmpty())
			{
				return false;
			}
			foreach (var path in paths)
			{
				(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = Load(path, multiple);
				foreach (var assembly in assemblies)
				{
					await AddToHome(_pluginCreator.CreateIPluginsFromAssembly(assembly), assembly);
				}
				pluginLoadContext.Unload();
			}
			return true;
		}

		// put entire unloadable AssemblyLoadContext in a method to avoid caller holding on to the reference
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Tuple<PluginLoadContext, IEnumerable<Assembly>> Load(string path, LoadStrategy multiple)
		{
			var pathInfo = File.GetAttributes(path);

			if ((pathInfo & FileAttributes.Directory) == FileAttributes.Directory)
			{
				if (!Directory.Exists(path))
				{
					throw new PluginException($"[{nameof(Load)}] The given path does not exist, path: {path}");
				}

				if (multiple == LoadStrategy.Multiple)
				{
					return _pluginFinderService.GetAssembliesAndLoadContext(path);
				}
			}
			var (pluginLoadContext, assembly) = _pluginFinderService.GetAssemblyAndLoadContext(path);
			return new Tuple<PluginLoadContext, IEnumerable<Assembly>>(pluginLoadContext, new[] { assembly });
		}

		private async Task AddToHome(Dictionary<string, T> iPluginDictionary, Assembly assembly)
		{
			foreach (var (name, _) in iPluginDictionary)
			{
				var listOfIPlugins = iPluginDictionary.Values.ToList() as IEnumerable<IPlugin>
									 ?? throw new PluginException(
										 $"[AddToHome] Error converting list of {name} to list of IPlugin");

				var listOfPlugins = _pluginCreator.CreatePluginsFromIPlugins(listOfIPlugins, assembly.Location);
				var home = await _unitOfWork.HomeRepository.GetHome();

				foreach (var plugin in listOfPlugins.Where(plugin => home.CheckIfPluginExistAndHasHigherVersion(plugin)))
				{
					listOfPlugins.Remove(plugin);
				}

				home.AddPlugins(listOfPlugins);
			}
		}
	}
}
