using SmartHub.Domain;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.AppFolder.AppConfigParser
{
	/// <summary>
	/// This service maintaines the yaml config file for the smartHub application
	/// </summary>
	public interface IAppConfigService
	{
		/// <summary>
		/// Gets the app config from cache.
		/// </summary>
		/// <returns>Returns appConfig.</returns>
		AppConfig GetConfig();

		/// <summary>
		/// Updates the config-File and the cache with the new appConfig class.
		/// </summary>
		/// <param name="newAppConfig">The new appConfig class.</param>
		/// <returns>Returns the completed Task state.</returns>
		Task UpdateConfigAsync(AppConfig newAppConfig);

		/// <summary>
		/// Creates a new Config file or reads an existing one and saves it to the cache.
		/// </summary>
		void CreateOrGetConfigFile();

		/// <summary>
		/// Saves the current appConfig class the the config-File.
		/// </summary>
		/// <returns>The completed Task state.</returns>
		Task SaveConfigAsync();

		/// <summary>
		/// Validates the given yaml string/File
		/// </summary>
		//Task ValidateConfigFile();
	}
}
