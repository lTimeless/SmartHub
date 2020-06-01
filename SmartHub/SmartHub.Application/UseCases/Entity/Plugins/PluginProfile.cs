using AutoMapper;
using SmartHub.Domain.Entities.Plugins;

namespace SmartHub.Application.UseCases.Entity.Plugins
{
	public class PluginProfile : Profile
	{
		public PluginProfile()
		{
			CreateMap<Plugin, PluginDto>()
				.ReverseMap(); // bidirectional mapping
		}
	}
}
