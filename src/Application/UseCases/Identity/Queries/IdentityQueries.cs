using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Enums;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Queries
{
	[Authorize]
	[GraphQLDescription("All queries for the me services.")]
	public class IdentityQueries
	{
		/// <summary>
		///     Retrieves my own user object.
		/// </summary>
		/// <exception cref="ArgumentNullException">Throws if current userName is null.</exception>
		/// <returns>Returns my user object or throws error if the needed info can't be retrieved from jwt.</returns>
		[GraphQLName("getMe")]
		public async Task<IdentityPayload> GetMeAsync([Service] IIdentityService identityService,
			[Service] ICurrentUserService currentUserService)
		{
			var userName = currentUserService.GetCurrentUsername();
			if (userName is null)
			{
				return new(new("Error: Couldn't retrieve profile for username information.", AppErrorCodes.NotFound));
			}

			var user = await identityService.FindByNameAsync(userName);
			if (user is null)
			{
				return new(new($"Error: Couldn't retrieve profile for username {userName}.", AppErrorCodes.NotFound));
			}

			return new(user);
		}

		/// <summary>
		///     logs out the current user.
		/// </summary>
		/// <returns>Returns the identityPayload with info that the user is now logged out.</returns>
		[GraphQLName("logout")]
		public async Task<IdentityPayload> LogoutAsync([Service] IIdentityService identityService,
			[Service] ICurrentUserService currentUserService)
		{
			var tokenCookies = currentUserService.GetTokenCookies();
			if (tokenCookies is null)
			{
				return new(new("Error: User is not logged in.", AppErrorCodes.NotSet));
			}

			var res = currentUserService.DeleteTokenCookies();
			return new(!res, "User logged out.");
		}
	}
}