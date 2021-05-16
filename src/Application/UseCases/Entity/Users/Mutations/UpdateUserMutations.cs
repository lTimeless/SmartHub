using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Users.Mutations
{
	/// <summary>
	/// Endpoint for update user.
	/// </summary>
	[Authorize]
	[GraphQLDescription("Endpoint for update user.")]
	public class UpdateUserMutations
	{
		/// <summary>
		/// Updates the user with the given input data.
		/// </summary>
		/// <param name="identityService">The identity service .</param>
		/// <param name="unitOfWork"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task<UserPayload> UpdateUser([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork, UpdateUserInput input)
		{
			var userEntity = await identityService.FindByIdAsync(input.UserId);
			if (userEntity == null)
			{
				return new(new($"Couldn't find user with {input.UserName}.", AppErrorCodes.NotFound));
			}

			userEntity.UserName = !string.IsNullOrEmpty(input.UserName) && !string.IsNullOrWhiteSpace(input.UserName)
				? input.UserName
				: userEntity.UserName;
			userEntity.PersonInfo = input.PersonInfo ?? userEntity.PersonInfo;
			userEntity.PersonName.FirstName = input.FirstName ?? userEntity.PersonName.FirstName;
			userEntity.PersonName.MiddleName = input.MiddleName ?? userEntity.PersonName.MiddleName;
			userEntity.PersonName.LastName = input.LastName ?? userEntity.PersonName.LastName;
			userEntity.Email = input.Email ?? userEntity.Email;
			userEntity.PhoneNumber = input.PhoneNumber ?? userEntity.PhoneNumber;

			var updateUser = await identityService.UpdateUserAsync(userEntity);
			if (!updateUser)
			{
				return new(new($"Error: Something went wrong updating user {userEntity.UserName}.", AppErrorCodes.NotUpdated));
			}

			if (string.IsNullOrEmpty(input.NewRole))
			{
				return new(userEntity);
			}
			var currentUserRoles = await identityService.GetUserRolesAsync(userEntity);
			// checks if currentRoles List has an entry and if that one is equal to the new role
			if (!currentUserRoles.Except(new List<string> {input.NewRole}).Any())
			{
				return new(userEntity);
			}

			var changeRole = await identityService.UserChangeRoleAsync(userEntity, input.NewRole);
			if (changeRole)
			{
				await unitOfWork.SaveAsync();
				return new(userEntity);
			}
			return new(new($"Error: Something went wrong updating user and Role for {input.UserName}.", AppErrorCodes.NotUpdated));
		}
	}
}