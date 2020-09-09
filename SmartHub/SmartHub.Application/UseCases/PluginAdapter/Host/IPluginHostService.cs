using System.Threading.Tasks;
using SmartHub.BasePlugin;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	/// <summary>
	/// Service for holding infos about available plugins
	/// Here you can load a specific plugin
	/// </summary>
	public interface IPluginHostService
	{
		Task<TP> GetPluginByNameAsync<TP>(string pluginName) where TP : IPlugin;

		/// <summary>
		/// Synchronizes the Dictionary and Database with the Plugin folder
		/// </summary>
		/// <returns>returns false if there was an error and true if it was successful</returns>
		Task<bool> SynchronizeDictionaryAndDb();
	}
}
