using SmartHub.Application.Common.Mappings;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.Entity.Homes
{
	public class AppDto : BaseDto, IMapFrom<Domain.AppConfig>
    {
    }
}