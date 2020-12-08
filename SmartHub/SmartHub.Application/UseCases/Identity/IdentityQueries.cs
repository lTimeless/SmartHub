// unset

using HotChocolate;
using SmartHub.Application.Common.Extensions;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity
{
	public class IdentityQueries
	{
		/// <summary>
		/// Retrieves my own user object.
		/// </summary>
		/// <returns>Returns my user object or throws error if the needed info can't be retrieved from jwt.</returns>
		[UseCurrentUser]
		public async Task<User?> GetMe([Service] IUserRepository userRepository, [Service] CurrentUser currentUser)
		{
			if (currentUser == null || (currentUser.User == null && currentUser.RequesterName == null))
			{
				throw new ArgumentNullException(nameof(currentUser));
			}

			var name = currentUser.User?.UserName ?? currentUser.RequesterName;
			return name is null
				? null
				: await userRepository.FindByNameAsync(name);
		}

	}
}