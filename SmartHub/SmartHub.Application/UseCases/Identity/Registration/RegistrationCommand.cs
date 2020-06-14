
using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	public class RegistrationCommand : IRequest<ServiceResponse<AuthResponseDto>>
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public RegistrationCommand(string username, string password, string role)
		{
			Username = username;
			Password = password;
			Role = role;
		}
	}
}
