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
		public async Task<IdentityPayload> GetMe([Service] IIdentityService identityService,
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

		public async Task<IdentityPayload> IsAuthenticated([Service] IIdentityService identityService,
			[Service] ICurrentUserService currentUserService)
		{
			// TODO hier dann nach cookies schauen oder im context nach dem jwt schauen und wenn die vorhanden sind dann true returnen
			// wenn nichts vorhanden ist dann false returnen
			// dieser endpoint wird zur selben zeit aufgerufen wie "homeExist" / beim start der website.

			var tokens = currentUserService.GetTokenCookies();
			if (tokens is null)
			{
				return new(new("Error: Not Authorized, please log in again.", AppErrorCodes.NotAuthorized));
			}

			// This logic in the identityService and reuse it at login and registration
			if (tokens.Item1 is null) // && or is expired
			{
				// Validate refreshToken
				// -> not valid: than create both tokens
				// -> valid: than create new jwt from refreshToken/Item2
			}


			return new(null);
		}
	}
}