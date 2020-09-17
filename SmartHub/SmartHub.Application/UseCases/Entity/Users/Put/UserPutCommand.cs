using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Users.Put
{
	public class UserPutCommand : IRequest<Response<UserDto>>
	{
		public string? UserName { get; set; }
		public string? PersonInfo { get; set; }
		public string? FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }

		public UserPutCommand(string? userName, string? personInfo, string? firstName, string? middleName, string? lastName, string? email, string? phoneNumber)
		{
			UserName = userName;
			PersonInfo = personInfo;
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
		}
	}
}
