using System;
using System.Globalization;
using AutoMapper;
using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Application.UseCases.Entity.Users
{
	public class UserDto : IMapFrom<User>
	{
		public string? UserName { get; init; }
		public string? PersonInfo { get; init; }
		public PersonName? PersonName { get; init; }
		public string? LastModifiedBy { get; init; }
		public string? LastModifiedAt { get; init; }
		public string? Email { get; init; }
		public string? PhoneNumber { get; init; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, UserDto>()
				.ForMember(x => x.LastModifiedAt, opt =>
					opt.MapFrom(x => x.LastModifiedAt
						.ToLocalTime()
						.ToString("g",
							CultureInfo.CurrentCulture)));

			profile.CreateMap<UserDto, User>()
				.ForMember(x => x.LastModifiedAt, opt =>
					opt.MapFrom(x => DateTimeOffset.Parse(x.LastModifiedAt ?? string.Empty)));
		}
	}
}
