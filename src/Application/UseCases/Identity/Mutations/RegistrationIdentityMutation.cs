using HotChocolate;
using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity.Mutations
{
	/// <summary>
	///     Endpoint for registration.
	/// </summary>
	[GraphQLDescription("Endpoint for registration.")]
	public class RegistrationIdentityMutation
	{
		/// <summary>
		///     Handles the registration process.
		/// </summary>
		/// <param name="identityService">The identity service.</param>
		/// <param name="unitOfWork">The unit of work.</param>
		/// <param name="accessor">The http context accessor.</param>
		/// <param name="input">The input the user does.</param>
		/// <returns>The payload with requested data.</returns>
		public async Task<IdentityPayload> Registration([Service] IIdentityService identityService,
			[Service] IUnitOfWork unitOfWork,
			[Service] IHttpContextAccessor accessor,
			RegistrationInput input)
		{
			var (userName, password, role) = input;
			var foundUser = await identityService.FindByNameAsync(userName);
			if (foundUser != null)
			{
				return new(new("Error: Username already exists.", AppErrorCodes.Exists));
			}

			var newUser = new User(userName, "");
			var result = await identityService.CreateUserAsync(newUser, password, role);

			if (!result)
			{
				return new(new($"Error: Could not register user {userName}", AppErrorCodes.NotCreated));
			}

			await unitOfWork.SaveAsync();

			var (token, refreshToken) = await identityService.CreateTokensAsync(newUser, new() {role});
			accessor.HttpContext.Response.Cookies.Append("SmartHub-Access-Token", token,
				new() {HttpOnly = true, SameSite = SameSiteMode.Strict});
			accessor.HttpContext.Response.Cookies.Append("SmartHub-Refresh-Token", refreshToken.Token,
				new() {HttpOnly = true, SameSite = SameSiteMode.Strict});

			return new(newUser);
		}
	}
}