using SmartHub.Domain;
using YamlDotNet.Serialization;

namespace SmartHub.Application.UseCases.HomeFolder
{
	public class YamlConfigStructure
	{
		public Domain.AppConfig? ApplicationConfig { get; set; }
	}
}