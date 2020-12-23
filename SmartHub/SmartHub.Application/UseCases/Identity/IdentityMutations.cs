using HotChocolate;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity
{
	public class IdentityMutations
	{
		/// <summary>
		/// Handles the login process.
		/// </summary>
		/// <param name="userManager"></param>
		/// <param name="identityService"></param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="configService"></param>
		/// <param name="input"></param>
		/// <returns>The payload with requested data.</returns>
		public async Task<IdentityPayload> Login([Service] UserManager<User> userManager,
			[Service] IdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			[Service] IAppConfigService configService,
			LoginInput input)
		{
			if (configService.GetConfig().IsActive is false)
			{
				return new IdentityPayload(new UserError("There is no home created yet.", AppErrorCodes.Exists));
			}

			var foundUser = await userManager.FindByNameAsync(input.UserName);
			if (foundUser == null)
			{
				return new IdentityPayload(new UserError("You are not authorized.", AppErrorCodes.Exists));
			}
			var result = await identityService.LoginAsync(input, foundUser);
			if (!result)
			{
				return new IdentityPayload(
					new UserError($"Error: Couldn't sign in user with username {input.UserName}", AppErrorCodes.Exists));
			}
			var rolesToUser = await userManager.GetRolesAsync(foundUser);
			if (foundUser.IsFirstLogin is false)
			{
				return identityService.CreateAuthResponse(foundUser, rolesToUser.ToList());
			}

			foundUser.IsFirstLogin = false;
			await unitOfWork.SaveAsync();
			return identityService.CreateAuthResponse(foundUser, rolesToUser.ToList());

		}

		/// <summary>
		/// Handles the registration process.
		/// </summary>
		/// <param name="identityService">The identity service.</param>
		/// <param name="userRepository">The user repository.</param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="input">The input the user does.</param>
		/// <returns>The payload with requested data.</returns>
		public async Task<IdentityPayload> Registration([Service] IdentityService identityService,
			[Service] IUserRepository userRepository, [Service] IUnitOfWork unitOfWork,
			RegistrationInput input)
		{
			var foundUser = await userRepository.FindByNameAsync(input.UserName);
			if (foundUser != null)
			{
				return new IdentityPayload(new UserError("Username already exists.", AppErrorCodes.Exists));
			}
			var newUser = new User(input.UserName, "");
			var result = await identityService.RegisterAsync(input, newUser);

			if (result)
			{
				await unitOfWork.SaveAsync();
				return identityService.CreateAuthResponse(newUser, new List<string> {input.Role});
			}
			return new IdentityPayload(new UserError($"Error: Could not register user {input.UserName}", AppErrorCodes.NotCreated));
		}
	}
}