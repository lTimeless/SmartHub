using AutoMapper;
using SmartHub.Application.UseCases.Entity.Homes.Create;
using SmartHub.Application.UseCases.Entity.Homes.Read;
using SmartHub.Domain.Entities.Homes;

namespace SmartHub.Application.UseCases.Entity.Homes
{
	public class HomeProfile : Profile
	{
		public HomeProfile()
		{
			// <Input, Output>
			CreateMap<Home, HomeReadResponseDto>()
				.PreserveReferences()
				.ReverseMap(); // bidirectional mapping

			CreateMap<Home, HomeCreateResponseDto>()
				.PreserveReferences()
				.ReverseMap();
		}
	}
}
