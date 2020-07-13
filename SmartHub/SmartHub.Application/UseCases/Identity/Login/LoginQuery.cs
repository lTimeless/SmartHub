using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginQuery : IRequest<ServiceResponse<AuthResponseDto>>
	{
		public string UserName { get; }
		public string Password { get; }

		public LoginQuery(string username, string password)
		{
			UserName = username;
			Password = password;
		}
	}
}
