using SmartHub.Domain.Entities;
using System;

namespace SmartHub.Application.Common.Interfaces
{
	/// <summary>
	///     Service for retrieving information about the current user from the HttpContext.
	/// </summary>
	public interface ICurrentUserService
	{
		/// <summary>
		///     Gets the current userName.
		/// </summary>
		/// <returns>
		///     If no user is made the request the name 'Home' will be returned- because than the home did something
		///     automatically.
		/// </returns>
		string? GetCurrentUsername();

		/// <summary>
		///     Gets the accessToken(jwt) and the refreshToken from the cookies.
		/// </summary>
		/// <returns>If the one of them is null than it returns null.</returns>
		Tuple<string, string>? GetTokenCookies();

		/// <summary>
		///     Sets the accessToken(jwt) and the refreshToken in the cookies.
		/// </summary>
		/// <returns>If the one of them is null than it returns null.</returns>
		void SetTokenCookies(string token, RefreshToken refreshToken);

		/// <summary>
		///     Gets the accessToken(jwt) and the refreshToken from the cookies.
		/// </summary>
		/// <returns>If the one of them is null than it returns null.</returns>
		bool DeleteTokenCookies();
	}
}