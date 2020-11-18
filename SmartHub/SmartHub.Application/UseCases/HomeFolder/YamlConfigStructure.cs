using SmartHub.Domain.Common;
using YamlDotNet.Serialization;

namespace SmartHub.Application.UseCases.HomeFolder
{
	public class YamlConfigStructure
	{
		public ApplicationConfig? ApplicationConfig { get; set; }
	}
}