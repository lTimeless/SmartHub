using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.PluginAdapter.Creator;
using SmartHub.Application.UseCases.PluginAdapter.Helper;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Extensions;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	/// <inheritdoc cref="IPluginHostService"/>
	public class PluginHostService : IPluginHostService
	{

		// Ein plugin/package ist eine assembly und beinhaltet funktionen in mehreren classen die dann weitere spezifizierte plugins sind
		// IPlugin = eine klasse
		private static readonly Dictionary<string, IPlugin> PluginsDictionary = new Dictionary<string, IPlugin>();

		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginLoadService _pluginLoadService;
		private readonly IPluginCreatorService _pluginCreatorService;
		private readonly ILogger _logger = Log.ForContext(typeof(PluginHostService));
		public PluginHostService(IUnitOfWork unitOfWork, IPluginLoadService pluginLoadService, IPluginCreatorService pluginCreatorService)
		{
			_unitOfWork = unitOfWork;
			_pluginLoadService = pluginLoadService;
			_pluginCreatorService = pluginCreatorService;
		}

		/// <inheritdoc cref="IPluginHostService.GetPluginByNameAsync{TP}"/>
		public async Task<TP> GetPluginByNameAsync<TP>(string pluginName) where TP : IPlugin
		{
			// the pluginName is a className inside of the dll
			if (string.IsNullOrEmpty(pluginName))
			{
				throw new PluginException($"Error: The given pluginName is null or empty - {pluginName}");
			}

			if (PluginsDictionary.TryGetValue(pluginName, out var iPlugin))
			{
				return (TP)iPlugin;
			}

			var home = await _unitOfWork.HomeRepository.GetHome();
			if (home is null)
			{
				throw new PluginException("Error: There is no home created at the moment");
			}
			var setting = home.Settings.FirstOrDefault(c => c.IsActive);

			var foundIplugin= await _pluginLoadService.LoadByName(pluginName, setting.PluginPath);
			PluginsDictionary[foundIplugin.Name] = foundIplugin; // add or update key
			_logger.Information($"Loaded {pluginName} from folder and added it to the dictionary.");
			return (TP)PluginsDictionary[pluginName];
		}

		/// <inheritdoc cref="IPluginHostService.SynchronizeDictionaryAndDb"/>
		public async Task<bool> SynchronizeDictionaryAndDb()
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			if (home is null)
			{
				_logger.Warning("No home available.");
				return false;
			}
			var setting = home.Settings.FirstOrDefault(c => c.IsActive);

			var foundPlugins = _pluginLoadService.FindPluginsInAssemblies(setting.PluginPath);
			var onlyNewPlugins = PluginHelper.FilterByFunction(foundPlugins, key => home.Plugins.Any(x => x.Name == key));

			if (onlyNewPlugins.IsNullOrEmpty())
			{
				_logger.Warning("No plugins available to synchronize.");
				return false;
			}

			foreach (var newPluginPath in onlyNewPlugins.Values.Select(x => x.Path).Distinct())
			{
				var newIPluginsDictionary = await _pluginLoadService.LoadAndCreateIPlugins(newPluginPath);
				var listOfPlugins = _pluginCreatorService.CreatePluginsFromIPlugins(newIPluginsDictionary.Values, newPluginPath);
				foreach (var plugin in listOfPlugins.Where(plugin => home.CheckIfPluginExistAndHasHigherVersion(plugin)))
				{
					listOfPlugins.Remove(plugin);
				}

				home.AddPlugins(listOfPlugins);
				foreach (var (key, value) in newIPluginsDictionary)
				{
					PluginsDictionary[key] = value; // Add or update key
				}
			}
			_logger.Information("Synchronized dictionary and database with plugin folder.");
			return true;
		}
	}
}
