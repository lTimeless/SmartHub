using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Mutations
{
	/// <summary>
	/// Endpoint for registration.
	/// </summary>
	[GraphQLDescription("Endpoint for registration.")]
	public class RegistrationIdentityMutation
	{
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