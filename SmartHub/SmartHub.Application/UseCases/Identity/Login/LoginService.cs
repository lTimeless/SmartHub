using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities.Users;
using SmartHub.Domain.Enums;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Login
{
	public class LoginService : ILoginService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ITokenGenerator _tokenGenerator;
		private readonly IChannelManager _channelManager;

		public LoginService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenGenerator tokenGenerator, IChannelManager channelManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenGenerator = tokenGenerator;
			_channelManager = channelManager;
		}

		public async Task<AuthResponseDto> LoginAsync(LoginQuery userInput)
		{
			var foundUser = await _userManager.FindByNameAsync(userInput.UserName);
			if (foundUser == null)
			{
				throw new RestException(HttpStatusCode.Unauthorized);
			}
			var result = await _signInManager.CheckPasswordSignInAsync(foundUser, userInput.Password, false);

			if (result.Succeeded)
			{
				var rolesToUser = await _userManager.GetRolesAsync(foundUser);
				await _channelManager.PublishNextToChannel(ChannelEventEnum.Events, new LoginEvent(foundUser.UserName, result.Succeeded));
				return new AuthResponseDto(_tokenGenerator.CreateJwtToken(foundUser),
							   foundUser.UserName,
							   rolesToUser.ToList(),
							   DateTime.Now.AddHours(JwtExpireTimeEnum.HoursToExpire.GetValue())
							   );
			}

			throw new RestException(HttpStatusCode.Unauthorized);
		}
	}
}
