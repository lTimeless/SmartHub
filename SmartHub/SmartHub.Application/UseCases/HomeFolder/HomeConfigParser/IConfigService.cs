namespace SmartHub.Application.UseCases.HomeFolder.HomeConfigParser
{
	public interface IConfigService
	{
		/// <summary>
		/// Creates the yaml-config file for the smarthome
		/// </summary>
		void CreateYamlConfigFile();

		/// <summary>
		/// Reads the yaml-config file for the smarthome
		/// </summary>
		void ReadYamlConfigFile();
	}
}
