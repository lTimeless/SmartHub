using AutoMapper;
using SmartHub.Domain.Entities.Users;

namespace SmartHub.Application.UseCases.Entity.Users
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.PreserveReferences()
				.ReverseMap();
		}
	}
}
