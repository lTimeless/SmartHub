using HotChocolate;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Mutations
{
	/// <summary>
	/// Endpoint for login.
	/// </summary>
	[GraphQLDescription("Endpoint for login.")]
	public class LoginIdentityMutation
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
	}
}