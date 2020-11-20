using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces;
using System.Threading.Tasks;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using Microsoft.Extensions.Options;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.Identity.Login
{
	/// <inheritdoc cref="ILoginService"/>
	public class LoginService : ILoginService
	{
		private readonly SignInManager<User> _signInManager;
		private readonly IChannelManager _channelManager;
		private readonly IOptionsSnapshot<AppConfig> _appConfig;

		public LoginService(SignInManager<User> signInManager, IChannelManager channelManager, IOptionsSnapshot<AppConfig> appConfig)
		{
			_signInManager = signInManager;
			_channelManager = channelManager;
			_appConfig = appConfig;
		}

		/// <inheritdoc cref="ILoginService.LoginAsync"/>
		public async Task<bool> LoginAsync(LoginQuery userInput, User foundUser)
		{
			if (_appConfig.Value.IsActive is false)
			{
				return false;
			}

			var result = await _signInManager.CheckPasswordSignInAsync(foundUser, userInput.Password, false);
			if (!result.Succeeded)
			{
				return false;
			}
			await _channelManager.PublishNextToChannel(ChannelNames.System,
				new IdentityEvent(foundUser.UserName,
					result.Succeeded,
			EventTypes.Login));
			return true;
		}
	}
}
