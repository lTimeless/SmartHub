using MediatR;
using SmartHub.Domain.Common;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginHandler : IRequestHandler<LoginQuery, ServiceResponse<AuthResponseDto>>
	{
		private readonly ILoginService _loginService;

		public LoginHandler(ILoginService loginService)
		{
			_loginService = loginService;
		}

		public async Task<ServiceResponse<AuthResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var result = await _loginService.LoginAsync(request).ConfigureAwait(false);
			return new ServiceResponse<AuthResponseDto>(result, true, "Successful");
		}
	}
}
