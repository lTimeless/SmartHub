using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using SmartHub.Application.Common.Attributes;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Queries
{
	[Authorize]
	[ExtendObjectType(Name = "RootQueries")]
	[GraphQLDescription("All queries for the me services.")]
	public class IdentityQueries
	{
		/// <summary>
		/// Retrieves my own user object.
		/// </summary>
		/// <returns>Returns my user object or throws error if the needed info can't be retrieved from jwt.</returns>
		[UseCurrentUser]
		public async Task<User?> GetMe([Service] IIdentityService identityService, [Service] CurrentUser currentUser)
		{
			if (currentUser == null || (currentUser.User == null && currentUser.RequesterName == null))
			{
				throw new ArgumentNullException(nameof(currentUser));
			}

			var name = currentUser.User?.UserName ?? currentUser.RequesterName;
			return name is null
				? null
				: await identityService.FindByNameAsync(name);
		}

	}
}