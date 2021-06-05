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
		[GraphQLName("GetMe")]
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
	}
}