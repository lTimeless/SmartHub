using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces;
using System.Threading.Tasks;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity.Login
{
	/// <inheritdoc cref="ILoginService"/>
	public class LoginService : ILoginService
	{
		private readonly SignInManager<User> _signInManager;
		private readonly IChannelManager _channelManager;

		public LoginService(SignInManager<User> signInManager, IChannelManager channelManager)
		{
			_signInManager = signInManager;
			_channelManager = channelManager;
		}

		/// <inheritdoc cref="ILoginService.LoginAsync"/>
		public async Task<bool> LoginAsync(LoginQuery userInput, User foundUser)
		{
			var result = await _signInManager.CheckPasswordSignInAsync(foundUser, userInput.Password, false);
			if (!result.Succeeded)
			{
				return false;
			}
			await _channelManager.PublishNextToChannel(EventTypes.Login, new LoginEvent(foundUser.UserName, result.Succeeded));
			return true;
		}
	}
}
