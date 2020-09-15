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
	}
}
