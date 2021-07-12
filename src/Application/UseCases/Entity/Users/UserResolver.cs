using HotChocolate;
using HotChocolate.Types;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Users
{
	[ExtendObjectType(nameof(User))]
	public class UserResolver
	{
		public async Task<IEnumerable<string>> GetRoles([Parent] User user, [Service] IIdentityService identityService)
		{
			return await identityService.GetUserRolesAsync(user);
		}
	}
}