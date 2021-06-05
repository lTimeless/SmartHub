using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using System;
using System.Security.Claims;

namespace SmartHub.WebUI.Services
{
	/// <inheritdoc cref="ICurrentUserService" />
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		/// <inheritdoc cref="ICurrentUserService.GetCurrentUsername" />
		public string? GetCurrentUsername()
		{
			return _httpContextAccessor
				.HttpContext?
				.User
				.FindFirstValue(ClaimTypes.Name);
		}

		/// <inheritdoc cref="ICurrentUserService.GetTokenCookies" />
		public Tuple<string, string>? GetTokenCookies()
		{
			var token = _httpContextAccessor.HttpContext?.Request.Cookies["SmartHub-Access-Token"];
			var reToken = _httpContextAccessor.HttpContext?.Request.Cookies["SmartHub-Refresh-Token"];
			if (reToken is null || token is null)
			{
				return null;
			}

			return new(token, reToken);
		}
	}
}