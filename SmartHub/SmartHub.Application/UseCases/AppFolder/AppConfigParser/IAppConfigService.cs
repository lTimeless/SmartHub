using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.AppFolder.AppConfigParser
{
	/// <summary>
	/// This service maintaines the yaml config file for the smartHub application
	/// </summary>
	public interface IAppConfigService
	{
		/// <summary>
		/// Creates a new Config file or reads an existing one and saves it to HomeConfiguration
		/// </summary>
		Task CreateOrReadConfigFile();

		/// <summary>
		/// Reads the yaml-config file for the smarthome.
		/// </summary>
		/// <returns>Return the current config file as string.</returns>
		Task<string> ReadConfigFileAsString();

		// TODO add logic
		/// <summary>
		/// Validates the given yaml string/File
		/// </summary>
		Task ValidateConfigFile();

		/// <summary>
		/// This updates the yamlFile with all properties in the Application class.
		/// Basically it overrides the file with the new properties.
		/// </summary>
		/// <returns>Returns true if it was successful.</returns>
		Task<bool> UpdateFileFromClass();
	}
}
