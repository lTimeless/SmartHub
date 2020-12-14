using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Users
{
	/// <summary>
	/// Endpoint for all user mutations.
	/// </summary>
	public class UserMutations
	{
		/// <summary>
		/// Updates the user with the given input data.
		/// </summary>
		/// <param name="userRepository">The user repository.</param>
		/// <param name="unitOfWork"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task<UserPayload> UpdateUser([Service] IUserRepository userRepository,
			[Service] IUnitOfWork unitOfWork, UpdateUserInput input)
		{
			var userEntity = await userRepository.FindByIdAsync(input.UserId);
			if (userEntity == null)
			{
				return new UserPayload(new UserError($"Couldn't find user with {input.UserName}.", AppErrorCodes.NotFound));
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

			var updateUser = await userRepository.UpdateUser(userEntity);
			if (!updateUser)
			{
				return new UserPayload(
					new UserError($"Error: Something went wrong updating user {userEntity.UserName}.", AppErrorCodes.NotUpdated));
			}

			if (string.IsNullOrEmpty(input.NewRole))
			{
				return new UserPayload(userEntity);
			}
			var currentUserRoles = await userRepository.GetUserRoles(userEntity);
			// checks if currentRoles List has an entry and if that one is equal to the new role
			if (!currentUserRoles.Except(new List<string> {input.NewRole}).Any())
			{
				return new UserPayload(userEntity);
			}

			var changeRole = await userRepository.UserChangeRole(userEntity, input.NewRole);
			if (changeRole)
			{
				await unitOfWork.SaveAsync();
				return new UserPayload(userEntity);
			}
			return new UserPayload(
					new UserError($"Error: Something went wrong updating user and Role for {input.UserName}.", AppErrorCodes.NotUpdated));
		}
	}
}