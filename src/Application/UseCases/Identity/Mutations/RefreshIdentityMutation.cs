using HotChocolate;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Enums;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Mutations
{
	/// <summary>
	///     Endpoint for RefreshIdentity.
	/// </summary>
	[GraphQLDescription("Endpoint for refresh identity.")]
	public class RefreshIdentityMutation
	{
		/// <summary>
		///     Handles the refreshing process of the user identity.
		/// </summary>
		/// <param name="identityService">The identity service.</param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="currentUserService">The service for retrieving the current user.</param>
		/// <returns>The payload with requested data.</returns>
		[GraphQLName("refreshTokens")]
		public async Task<IdentityPayload> RefreshTokensAsync([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			[Service] ICurrentUserService currentUserService)
		{
			var tokens = currentUserService.GetTokenCookies();
			if (tokens is null)
			{
				return new(new("Error: Not Authorized, please log in again.", AppErrorCodes.NotAuthorized));
			}

			var newTokens = await identityService.RefreshTokensAsync(tokens.Item1, tokens.Item2);
			if (newTokens == null)
			{
				return new(new("Error: Not Authorized, please log in again.", AppErrorCodes.NotAuthorized));
			}

			await unitOfWork.SaveAsync();
			currentUserService.SetTokenCookies(newTokens.Item1, newTokens.Item2);

			return new(newTokens.Item2.User, "User is authenticated.");
		}
	}
}