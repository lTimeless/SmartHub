using HotChocolate;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Domain.Common.Enums;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Mutations
{
	/// <summary>
	///     Endpoint for login.
	/// </summary>
	[GraphQLDescription("Endpoint for login.")]
	public class LoginIdentityMutation
	{
		/// <summary>
		///     Handles the login process.
		/// </summary>
		/// <param name="identityService">The identity service.</param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="configService">The service for the smartHub config.</param>
		/// <param name="currentUserService">The current user service.</param>
		/// <param name="input">The input values.</param>
		/// <returns>The payload with requested data.</returns>
		[GraphQLName("login")]
		public async Task<IdentityPayload> LoginAsync([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			[Service] IAppConfigService configService,
			[Service] ICurrentUserService currentUserService,
			LoginInput input)
		{
			if (configService.GetConfig().IsActive is false)
			{
				return new(new("Error: There is no home created yet.", AppErrorCodes.NoHome));
			}

			var (userName, password) = input;
			var foundUser = await identityService.FindByNameAsync(userName);
			if (foundUser == null)
			{
				return new(new($"Error: No user found with name - {userName}", AppErrorCodes.NotFound));
			}

			var result = await identityService.LoginAsync(foundUser, password);
			if (!result)
			{
				return new(new($"Error: Wrong password for user - {userName}", AppErrorCodes.NotAuthorized));
			}

			var (token, refreshToken) = await identityService.CreateTokensAsync(foundUser);
			currentUserService.SetTokenCookies(token, refreshToken);

			if (foundUser.IsFirstLogin is false)
			{
				await unitOfWork.SaveAsync();
				return new(foundUser);
			}

			foundUser.IsFirstLogin = false;
			await unitOfWork.SaveAsync();
			return new(foundUser);
		}
	}
}