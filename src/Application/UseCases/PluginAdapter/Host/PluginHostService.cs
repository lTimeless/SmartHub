using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Helper;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.BasePlugin;
using SmartHub.Domain;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities;
using System;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	/// <inheritdoc cref="IPluginHostService"/>
	public class PluginHostService : IPluginHostService
	{

		// Ein plugin/package ist eine assembly und beinhaltet funktionen in mehreren classen die dann weitere spezifizierte plugins sind
		// IPlugin = eine klasse
		private static readonly Dictionary<string, IPlugin> _pluginsDictionary = new();

		private readonly IAppConfigService _configService;
		private readonly IBaseRepositoryAsync<Plugin> _pluginRepository;
		private readonly IPluginLoadService _pluginLoadService;
		private readonly IPluginCreatorService _pluginCreatorService;
		private readonly ILogger _logger = Log.ForContext(typeof(PluginHostService));
		public PluginHostService(IPluginLoadService pluginLoadService, IPluginCreatorService pluginCreatorService, IAppConfigService configService, IBaseRepositoryAsync<Plugin> pluginRepository)
		{
			_pluginLoadService = pluginLoadService;
			_pluginCreatorService = pluginCreatorService;
			_configService = configService;
			_pluginRepository = pluginRepository;
		}

		/// <inheritdoc cref="IPluginHostService.GetPluginByNameAsync{TP}"/>
		public async Task<TP> GetPluginByNameAsync<TP>(string pluginName) where TP : IPlugin
		{
			// the pluginName is a className inside of the dll
			if (string.IsNullOrEmpty(pluginName))
			{
				throw new PluginException($"Error: The given pluginName is null or empty - {pluginName}");
			}

			if (_pluginsDictionary.TryGetValue(pluginName, out var iPlugin))
			{
				return (TP)iPlugin;
			}

			var appConfig = _configService.GetConfig();
			if (appConfig.IsActive is false)
			{
				throw new PluginException("Error: There is no home created at the moment");
			}

			var foundIplugin = await _pluginLoadService.LoadByName(pluginName, appConfig.PluginFolderPath);
			_pluginsDictionary[foundIplugin.Name] = foundIplugin; // add or update key
			_logger.Information($"Loaded {pluginName} from folder and added it to the dictionary.");
			return (TP)_pluginsDictionary[pluginName];
		}

		/// <inheritdoc cref="IPluginHostService.SynchronizeDictionaryAndDb"/>
		public async Task<bool> SynchronizeDictionaryAndDb()
		{
			var appConfig = _configService.GetConfig();
			if (appConfig.IsActive is false)
			{
				_logger.Warning("No home available.");
				return false;
			}
			var plugins = await _pluginRepository.GetAllAsync();
			var foundPlugins = _pluginLoadService.FindPluginsInAssemblies(appConfig.PluginFolderPath ?? string.Empty);
			var onlyNewPlugins = PluginHelper.FilterByFunction(foundPlugins, key => plugins.Any(x => x.Name == key));

			if (onlyNewPlugins.IsNullOrEmpty())
			{
				_logger.Warning("No plugins available to synchronize.");
				return false;
			}

			foreach (var newPluginPath in onlyNewPlugins.Values.Select(x => x.Path).Distinct())
			{
				var newIPluginsDictionary = await _pluginLoadService.LoadAndCreateIPlugins(newPluginPath);
				var listOfPlugins = _pluginCreatorService.CreatePluginsFromIPlugins(newIPluginsDictionary.Values, newPluginPath);


				// string comparer schreiben für plugin.AssemblyVersion
				// && x.AssemblyVersion > newPlugin.AssemblyVersion
				foreach (var plugin in listOfPlugins.Where(newPlugin => plugins.Exists(x => x.Name == newPlugin.Name)))
				{
					listOfPlugins.Remove(plugin);
				}

				var addedPlugins = await _pluginRepository.AddRangeAsync(listOfPlugins);
				if (addedPlugins is false)
				{
					throw new SmartHubException("Could not add a list of plugins to database.");
				}
				foreach (var (key, value) in newIPluginsDictionary)
				{
					_pluginsDictionary[key] = value; // Add or update key
				}
			}
			_logger.Information("Synchronized dictionary and database with plugin folder.");
			return true;
		}
	}
}
