using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Entities.Plugins;

namespace SmartHub.Application.UseCases.Entity.Plugins
{
	public class PluginDto : BaseDto , IMapFrom<Plugin>
	{
	}
}
