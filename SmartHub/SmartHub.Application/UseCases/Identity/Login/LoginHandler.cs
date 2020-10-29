using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginHandler : IRequestHandler<LoginQuery, Response<AuthResponseDto>>
	{
		private readonly UserManager<User> _userManager;
		private readonly ILoginService _loginService;
		private readonly IdentityService _identityService;

		public LoginHandler(ILoginService loginService, UserManager<User> userManager, IdentityService identityService)
		{
			_loginService = loginService;
			_userManager = userManager;
			_identityService = identityService;
		}

		public async Task<Response<AuthResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var foundUser = await _userManager.FindByNameAsync(request.UserName);
			if (foundUser == null)
			{
				return Response.Fail<AuthResponseDto>("You are not authorized.", new AuthResponseDto(string.Empty));
			}
			var result = await _loginService.LoginAsync(request, foundUser);

			if (!result)
			{
				return Response.Fail<AuthResponseDto>($"Error: Couldn't sign in user with username {request.UserName}", new AuthResponseDto(string.Empty));
			}
			var rolesToUser = await _userManager.GetRolesAsync(foundUser);
			return Response.Ok("Successful",
				_identityService.CreateAuthResponse(foundUser, rolesToUser.ToList()));
		}
	}
}
