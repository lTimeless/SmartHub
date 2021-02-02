using HotChocolate;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Domain.Common.Enums;
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
		/// <param name="identityService"></param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="configService"></param>
		/// <param name="input"></param>
		/// <returns>The payload with requested data.</returns>
		public async Task<IdentityPayload> Login([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			[Service] IAppConfigService configService,
			LoginInput input)
		{
			if (configService.GetConfig().IsActive is false)
			{
				return new(new("Error: There is no home created yet.", AppErrorCodes.NoHome));
			}

			var foundUser = await identityService.FindByNameAsync(input.UserName);
			if (foundUser == null)
			{
				return new(new($"Error: No user found with name - {input.UserName}", AppErrorCodes.NotFound));
			}

			var result = await identityService.LoginAsync(foundUser, input.Password);
			if (!result)
			{
				return new(new($"Error: Wrong password for user - {input.UserName}", AppErrorCodes.NotAuthorized));
			}
			var token = await identityService.CreateAuthResponse(foundUser);
			if (foundUser.IsFirstLogin is false)
			{
				return new(foundUser, token);
			}

			foundUser.IsFirstLogin = false;
			await unitOfWork.SaveAsync();
			return new(foundUser, token);
		}
	}
}