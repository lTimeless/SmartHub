using HotChocolate;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
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
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="input">The input the user does.</param>
		/// <returns>The payload with requested data.</returns>
		public async Task<IdentityPayload> Registration([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			RegistrationInput input)
		{
			var foundUser = await identityService.FindByNameAsync(input.UserName);
			if (foundUser != null)
			{
				return new(new("Error: Username already exists.", AppErrorCodes.Exists));
			}
			var newUser = new User(input.UserName, "");
			var result = await identityService.CreateUser(newUser, input.Password, input.Role);

			if (result)
			{
				await unitOfWork.SaveAsync();
				var token = await identityService.CreateAuthResponse(newUser, new() {input.Role});
				return new(newUser, token);
			}
			return new(new($"Error: Could not register user {input.UserName}", AppErrorCodes.NotCreated));
		}
	}
}