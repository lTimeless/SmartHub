using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginHandler : IRequestHandler<LoginQuery, Response<AuthResponseDto>>
	{
		private readonly ILoginService _loginService;

		public LoginHandler(ILoginService loginService)
		{
			_loginService = loginService;
		}

		public async Task<Response<AuthResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var result = await _loginService.LoginAsync(request);
			return Response.Ok( "Successful", result);
		}
	}
}
