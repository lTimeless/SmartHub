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

		public LoginService(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;
		}

		/// <inheritdoc cref="ILoginService.LoginAsync"/>
		public async Task<bool> LoginAsync(LoginQuery userInput, User foundUser)
		{
			var result = await _signInManager.CheckPasswordSignInAsync(foundUser, userInput.Password, false);
			return result.Succeeded;
		}
	}
}
