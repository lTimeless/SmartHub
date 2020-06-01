using MediatR;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginQuery : IRequest<ServiceResponse<AuthResponseDto>>
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		public LoginQuery(string username, string password)
		{
			UserName = username;
			Password = password;
		}
	}
}
