using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity.Login
{
	/// <inheritdoc cref="ILoginService"/>
	public class LoginService : ILoginService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IChannelManager _channelManager;
		private readonly IdentityService _identityService;

		public LoginService(UserManager<User> userManager, SignInManager<User> signInManager, IChannelManager channelManager, IdentityService identityService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_channelManager = channelManager;
			_identityService = identityService;
		}

		/// <inheritdoc cref="ILoginService.LoginAsync"/>
		public async Task<AuthResponseDto> LoginAsync(LoginQuery userInput)
		{
			var foundUser = await _userManager.FindByNameAsync(userInput.UserName);
			if (foundUser == null)
			{
				throw new RestException(HttpStatusCode.Unauthorized);
			}
			var result = await _signInManager.CheckPasswordSignInAsync(foundUser, userInput.Password, false);

			if (!result.Succeeded)
			{
				throw new RestException(HttpStatusCode.Unauthorized);
			}
			var rolesToUser = await _userManager.GetRolesAsync(foundUser);
			await _channelManager.PublishNextToChannel(EventTypes.Login, new LoginEvent(foundUser.UserName, result.Succeeded));
			return _identityService.CreateAuthResponse(foundUser, rolesToUser.ToList());
		}
	}
}
