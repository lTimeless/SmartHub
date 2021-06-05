using HotChocolate;
using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Domain.Common.Enums;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Mutations
{
	/// <summary>
	///     Endpoint for login.
	/// </summary>
	[GraphQLDescription("Endpoint for login.")]
	public class LoginIdentityMutation
	{
		/// <summary>
		///     Handles the login process.
		/// </summary>
		/// <param name="identityService">The identity service.</param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="configService">The service for the smartHub config.</param>
		/// <param name="accessor">The http context accessor.</param>
		/// <param name="input">The input values.</param>
		/// <returns>The payload with requested data.</returns>
		[GraphQLName("login")]
		public async Task<IdentityPayload> LoginAsync([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			[Service] IAppConfigService configService,
			[Service] IHttpContextAccessor accessor,
			[Service] ICurrentUserService currentUserService,
			LoginInput input)
		{
			if (configService.GetConfig().IsActive is false)
			{
				return new(new("Error: There is no home created yet.", AppErrorCodes.NoHome));
			}

			var (userName, password) = input;
			var foundUser = await identityService.FindByNameAsync(userName);
			if (foundUser == null)
			{
				return new(new($"Error: No user found with name - {userName}", AppErrorCodes.NotFound));
			}

			var result = await identityService.LoginAsync(foundUser, password);
			if (!result)
			{
				return new(new($"Error: Wrong password for user - {userName}", AppErrorCodes.NotAuthorized));
			}

			var (token, refreshToken) = await identityService.CreateTokensAsync(foundUser);
			accessor.HttpContext.Response.Cookies.Append("SmartHub-Access-Token", token,
				new() {HttpOnly = true, SameSite = SameSiteMode.Strict, MaxAge = TimeSpan.FromHours(1), Secure = true});
			accessor.HttpContext.Response.Cookies.Append("SmartHub-Refresh-Token", refreshToken.Token,
				new() {HttpOnly = true, SameSite = SameSiteMode.Strict, MaxAge = TimeSpan.FromDays(7), Secure = true});

			if (foundUser.IsFirstLogin is false)
			{
				await unitOfWork.SaveAsync();
				return new(foundUser);
			}

			foundUser.IsFirstLogin = false;
			await unitOfWork.SaveAsync();
			return new(foundUser);
		}

		[GraphQLName("refreshTokens")]
		public async Task<IdentityPayload> RefreshTokensAsync([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork, [Service] IHttpContextAccessor accessor,
			[Service] ICurrentUserService currentUserService)
		{
			var tokens = currentUserService.GetTokenCookies();
			if (tokens is null)
			{
				return new(new("Error: Not Authorized, please log in again.", AppErrorCodes.NotAuthorized));
			}

			var newTokens = await identityService.RefreshTokensAsync(tokens.Item1, tokens.Item2);
			if (newTokens == null)
			{
				return new(new("Error: Not Authorized, please log in again.", AppErrorCodes.NotAuthorized));
			}

			await unitOfWork.SaveAsync();

			accessor.HttpContext.Response.Cookies.Append("SmartHub-Access-Token", newTokens.Item1,
				new() {HttpOnly = true, SameSite = SameSiteMode.Strict, MaxAge = TimeSpan.FromHours(1), Secure = true});
			accessor.HttpContext.Response.Cookies.Append("SmartHub-Refresh-Token", newTokens.Item2,
				new() {HttpOnly = true, SameSite = SameSiteMode.Strict, MaxAge = TimeSpan.FromDays(7), Secure = true});

			return new(default, "User is authenticated.");
		}
	}
}