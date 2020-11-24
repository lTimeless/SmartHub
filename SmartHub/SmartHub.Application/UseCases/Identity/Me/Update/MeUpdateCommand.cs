using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Users;

namespace SmartHub.Application.UseCases.Identity.Me.Update
{
	public record MeUpdateCommand : IRequest<Response<UserDto>>
	{
		public string UserName { get; init; }
		public string PersonInfo { get; init; }
		public string FirstName { get; init; }
		public string MiddleName { get; init; }
		public string LastName { get; init; }
		public string Email { get; init; }
		public string PhoneNumber { get; init; }
		public string NewRole { get; init; }
	}
}
