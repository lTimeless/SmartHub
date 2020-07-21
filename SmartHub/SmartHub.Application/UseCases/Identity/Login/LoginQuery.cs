using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginQuery : IRequest<Response<AuthResponseDto>>
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
