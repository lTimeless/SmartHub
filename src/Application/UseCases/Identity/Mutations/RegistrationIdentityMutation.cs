using HotChocolate;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Mutations
{
	/// <summary>
	///     Endpoint for registration.
	/// </summary>
	[GraphQLDescription("Endpoint for registration.")]
	public class RegistrationIdentityMutation
	{
		/// <summary>
		///     Handles the registration process.
		/// </summary>
		/// <param name="identityService">The identity service.</param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="currentUserService">The current user service.</param>
		/// <param name="input">The input the user does.</param>
		/// <returns>The payload with requested data.</returns>
		public async Task<IdentityPayload> RegistrationAsync([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			[Service] ICurrentUserService currentUserService,
			RegistrationInput input)
		{
			var (userName, password, role) = input;
			var foundUser = await identityService.FindByNameAsync(userName);
			if (foundUser != null)
			{
				return new(new("Error: Username already exists.", AppErrorCodes.Exists));
			}

			var newUser = new User(userName, "");
			var result = await identityService.CreateUserAsync(newUser, password, role);

			if (!result)
			{
				return new(new($"Error: Could not register user {userName}", AppErrorCodes.NotCreated));
			}

			await unitOfWork.SaveAsync();

			var (token, refreshToken) = await identityService.CreateTokensAsync(newUser, new() {role});
			currentUserService.SetTokenCookies(token, refreshToken);
			return new(newUser);
		}
	}
}