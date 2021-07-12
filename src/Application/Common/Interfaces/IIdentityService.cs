using SmartHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
	/// <summary>
	///     Service for all auth stuff.
	/// </summary>
	public interface IIdentityService
	{
		/// <summary>
		///     Validates and logs in a user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The typed in password.</param>
		/// <returns>True if successful.</returns>
		Task<bool> LoginAsync(User user, string password);


		/// <summary>
		///     Looks if any user exists in the database.
		/// </summary>
		/// <returns>True if any user exist.</returns>
		Task<bool> UsersExistAsync();

		/// <summary>
		///     Searches for any user by a userName.
		/// </summary>
		/// <param name="username">The wanted userName.</param>
		/// <returns>The found user or null.</returns>
		Task<User?> FindByNameAsync(string username);

		/// <summary>
		///     Searches for any user by an id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>The found user or null.</returns>
		Task<User?> FindByIdAsync(string userId);

		/// <summary>
		///     Creates a new user with the given parameters.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="pw">The password.</param>
		/// <param name="roleName">The role.</param>
		/// <returns>True if successful.</returns>
		Task<bool> CreateUserAsync(User user, string pw, string roleName);

		/// <summary>
		///     Updates a user by the given parameters.
		/// </summary>
		/// <param name="user">The changed user.</param>
		/// <returns>True if successful.</returns>
		Task<bool> UpdateUserAsync(User user);

		/// <summary>
		///     Gets an IEnumerable of roles for the given user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>IEnumerable of roles.</returns>
		Task<IEnumerable<string>> GetUserRolesAsync(User user);

		/// <summary>
		///     Changes the role for the given user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="newRoleName">The new role.</param>
		/// <returns>True if successful.</returns>
		Task<bool> UserChangeRoleAsync(User user, string newRoleName);

		/// <summary>
		///     Creates a Tuple with the jwtToken and new or updated refreshToken.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="roles">The roles if the user doesn't already have some.</param>
		/// <param name="storedRefreshToken">The already existing and valid refreshToken.</param>
		/// <returns>Tuple with user and token.</returns>
		Task<Tuple<string, RefreshToken>> CreateTokensAsync(User user, List<string>? roles = default,
			RefreshToken? storedRefreshToken = default);

		Task<Tuple<string, RefreshToken>?> RefreshTokensAsync(string jwt, string refreshToken);
	}
}