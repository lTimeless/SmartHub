namespace SmartHub.Application.UseCases.AppFolder.AppConfigParser
{
	public interface IConfigService
	{
		/// <summary>
		/// Creates a new Config file or reads an existing one and saves it to HomeConfiguration
		/// </summary>
		void CreateOrReadConfigFile();

		/// <summary>
		/// Reads the yaml-config file for the smarthome.
		/// </summary>
		/// <returns>Return the current config file as string.</returns>
		string ReadConfigFileAsString();

		/// <summary>
		/// TODO:
		/// Validates the given yaml string/File
		/// </summary>
		void ValidateConfigFile();

		/// <summary>
		/// This updates the yamlFile with all properties in the Application class.
		/// Basically it overrides the file the the new properties.
		/// </summary>
		/// <returns>Returns true if it was successful.</returns>
		bool UpdateFileFromClass();
	}
}
