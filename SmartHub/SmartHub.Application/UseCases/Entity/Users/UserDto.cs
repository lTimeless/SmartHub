using System;

namespace SmartHub.Application.UseCases.Entity.Users
{
	public class UserDto
	{
		public string Id { get; set; }
		private DateTime? CreatedAt { get; set; }
		private DateTime? ModifiedDate { get; set; }

		public string? Description { get; set; }

		public string? PersonInfo { get; set; }

		public string UserName { get; set; }
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		//public HomeDto? HomeDto { get; set; }
	}
}
