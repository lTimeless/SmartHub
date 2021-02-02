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
using SmartHub.Application.UseCases.PluginAdapter.Helper;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
	/// <inheritdoc cref="IPluginLoadService"/>
	public class PluginLoadService: IPluginLoadService
	{
		private readonly IPluginCreatorService _pluginCreator;

		public PluginLoadService(IPluginCreatorService pluginCreator)
		{
			_pluginCreator = pluginCreator;
		}

		/// <inheritdoc cref="IPluginLoadService.LoadByName"/>
		public async Task<IPlugin> LoadByName(string pluginName, string pluginPath)
		{
			var foundAllFindPluginsInAssembliesDictionary = FindPluginsInAssemblies(pluginPath);
			if (!foundAllFindPluginsInAssembliesDictionary.ContainsKey(pluginName))
			{
				throw new PluginException($"Error: No plugin found, with name - {pluginName}");
			}

			var pluginDto = foundAllFindPluginsInAssembliesDictionary[pluginName];
			if (!PluginHelper.ValidatePath(pluginDto.Path))
			{
				throw new PluginException($"Error: Couldn't load plugin {pluginName}");
			}

			var iPluginsFromAssembly = await LoadAndCreateIPlugins(pluginDto.Path);

			if (iPluginsFromAssembly.IsNullOrEmpty())
			{
				throw new PluginException($"Error: While receiving the IPlugin from {nameof(_pluginCreator.CreateIPluginsFromAssembly)}");
			}

			var foundIPlugin = iPluginsFromAssembly.First(x => x.Key.Equals(pluginName));
			return foundIPlugin.Value;
		}

		/// <inheritdoc cref="IPluginLoadService.FindPluginsInAssemblies"/>
		public IReadOnlyDictionary<string, PluginDto> FindPluginsInAssemblies(string path)
		{
			var assemblyPluginInfos = new Dictionary<string, PluginDto>();
			(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = LoadAssembliesAndContext(path);

			var assBies = assemblies.ToList();
			foreach (var assembly in assBies)
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

		/// <inheritdoc cref="IPluginLoadService.LoadAndCreateIPlugins"/>
		public Task<Dictionary<string, IPlugin>> LoadAndCreateIPlugins(string pluginPath)
		{
			var assembly = LoadAssemblyBypath(pluginPath);
			return Task.FromResult(_pluginCreator.CreateIPluginsFromAssembly(assembly));
		}


		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Tuple<PluginLoadContext, IEnumerable<Assembly>> LoadAssembliesAndContext(string path)
		{
			var pluginLoadContext = new PluginLoadContext();
			var assemblies = Directory
				.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories)
				.Select(pluginLoadContext.LoadFromAssemblyPath)
				.Distinct();
			return new(pluginLoadContext, assemblies);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Assembly LoadAssemblyBypath(string path)
		{
			var pluginLoadContext = new PluginLoadContext();
			var assembly = pluginLoadContext.LoadFromAssemblyPath(path);
			if (assembly is null)
			{
				throw new PluginException($" Error: Could not load the assembly under the given path: {path}");
			}
			return assembly;
		}
	}
}
