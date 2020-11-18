namespace SmartHub.Application.UseCases.HomeFolder.HomeConfigParser
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

		void ValidateConfigFile();
		void UpdateConfigFileFromSystem();
		void UpdateConfigFileFromString();
	}
}
