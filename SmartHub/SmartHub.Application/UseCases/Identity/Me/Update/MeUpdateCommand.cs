using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Users;

namespace SmartHub.Application.UseCases.Identity.Me.Update
{
	public class MeUpdateCommand : IRequest<Response<UserDto>>
	{
		public string UserName { get; set; }
		public string PersonInfo { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string NewRole { get; set; }

		public MeUpdateCommand(string userName,
			string personInfo,
			string firstName,
			string middleName,
			string lastName,
			string email,
			string phoneNumber,
			string newRole)
		{
			UserName = userName;
			PersonInfo = personInfo;
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			NewRole = newRole;
		}
	}
}
