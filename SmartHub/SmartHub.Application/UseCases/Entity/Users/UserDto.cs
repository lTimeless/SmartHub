using System;
using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Entities.Users;

namespace SmartHub.Application.UseCases.Entity.Users
{
	public class UserDto : BaseDto , IMapFrom<User>
	{
		public string? PersonInfo { get; set; }

		public string UserName { get; set; }
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		//public HomeDto? HomeDto { get; set; }
	}
}
