using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	public class PluginHostService : IPluginHostService
	{

		// Ein plugin/package ist eine assembly und beinhaltet funktionen in mehreren classen die dann weitere spezifizierte plugins sind
		private static readonly Dictionary<string, IPlugin> PluginsDictionary = new Dictionary<string, IPlugin>();

		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginLoadService _pluginLoadService;
		public PluginHostService(IUnitOfWork unitOfWork, IPluginLoadService pluginLoadService)
		{
			_unitOfWork = unitOfWork;
			_pluginLoadService = pluginLoadService;
		}

		// the pluginName is a className inside of the dll
		public async Task<TP> GetPluginByNameAsync<TP>(string pluginName) where TP : IPlugin
		{
			if (string.IsNullOrEmpty(pluginName))
			{
				throw new PluginException($"Error: The given pluginName is null or empty - {pluginName}");
			}
			if (PluginsDictionary.ContainsKey(pluginName))
			{
				return (TP)PluginsDictionary[pluginName];
			}

			var foundIplugin= await _pluginLoadService.LoadByName(pluginName);
			PluginsDictionary.Add(foundIplugin.Name, foundIplugin);
			return (TP)PluginsDictionary[pluginName];
		}

		public Task SynchronizeDictionaryWithDb(string assemblyPath, LoadStrategy multiple)
		{
			// übernimmt die logic from loader für das hinzufügen
			// findallplugins
			// dann filter by homeplugins
			// gefiltertes Dictionary dem static dictionary und home hinzufügen
			throw new NotImplementedException();
		}

		// can be made private
		public Task AddToHome(string assemblyPath, LoadStrategy multiple)
		{
			throw new NotImplementedException();
		}

		// TODO: wird wegfallen, da alle welche nicht im dictionary sind und vom User angefragt werden, geladen werden.
		// und die AddTOHome function wird in neue Funktion "SynchronizeWithDb" umwandern

		/// <inheritdoc cref="IPluginLoadService.LoadAndAddToHomeAsync"/>
		// public async Task<bool> LoadAndAddToHomeAsync(string assemblyPath, LoadStrategy multiple)
		// {
		// 	var paths = assemblyPath.ToList();
		// 	if (paths.IsNullOrEmpty())
		// 	{
		// 		return false;
		// 	}
		// 	(PluginLoadContext pluginLoadContext, IEnumerable<Assembly> assemblies) = Load(assemblyPath, multiple);
		// 	foreach (var assembly in assemblies)
		// 	{
		// 		await AddToHome(_pluginCreator.CreateIPluginsFromAssembly(assembly), assembly);
		// 	}
		// 	pluginLoadContext.Unload();
		// 	return true;
		// }
		// private async Task AddToHome(Dictionary<string, IPlugin> iPluginDictionary, Assembly assembly)
		// {
		// 	foreach (var (name, _) in iPluginDictionary)
		// 	{
		// 		var listOfIPlugins = iPluginDictionary.Values.ToList() as IEnumerable<IPlugin>
		// 		                     ?? throw new PluginException(
		// 			                     $"[AddToHome] Error converting list of {name} to list of IPlugin");
		//
		// 		var listOfPlugins = _pluginCreator.CreatePluginsFromIPlugins(listOfIPlugins, assembly.Location);
		// 		var home = await _unitOfWork.HomeRepository.GetHome();
		//
		// 		foreach (var plugin in listOfPlugins.Where(plugin => home.CheckIfPluginExistAndHasHigherVersion(plugin)))
		// 		{
		// 			listOfPlugins.Remove(plugin);
		// 		}
		//
		// 		home.AddPlugins(listOfPlugins);
		// 	}
		// }
	}
}
