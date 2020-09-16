using AutoMapper;
using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Application.UseCases.Entity.Users
{
	public class UserDto : IMapFrom<User>
	{
		public string UserName { get; set; }
		public string? PersonInfo { get; set; }
		public PersonName? PersonName { get; set;  }
		public string LastModifiedBy { get; set; }
		public string LastModifiedAt { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool TwoFactorEnabled { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, UserDto>()
				.ForMember(x => x.LastModifiedAt, opt =>
					opt.MapFrom(x => x.LastModifiedAt.ToDateTimeOffset().ToString("G")))
				.ReverseMap();
		}
	}
}
