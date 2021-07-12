using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Options;
using SmartHub.Domain.Entities;
using System;
using System.Security.Claims;

namespace SmartHub.WebUI.Services
{
	/// <inheritdoc cref="ICurrentUserService" />
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly JwtOptions _jwtOptions;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor, IOptions<JwtOptions> options)
		{
			_httpContextAccessor = httpContextAccessor;
			_jwtOptions = options.Value;
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

		public void SetTokenCookies(string token, RefreshToken refreshToken)
		{
			if (_httpContextAccessor.HttpContext is null)
			{
				return;
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("SmartHub-Access-Token", token,
				new()
				{
					HttpOnly = true,
					SameSite = SameSiteMode.Lax,
					Expires = DateTimeOffset.Now.AddMinutes(_jwtOptions.LifeTimeInMinutes),
					Secure = true
				});

			_httpContextAccessor.HttpContext.Response.Cookies.Append("SmartHub-Refresh-Token", refreshToken.Token,
				new()
				{
					HttpOnly = true,
					SameSite = SameSiteMode.Lax,
					Expires = refreshToken.ExpirationDate,
					Secure = true
				});
		}

		public bool DeleteTokenCookies()
		{
			var tokenCookies = GetTokenCookies();
			if (tokenCookies is null)
			{
				return false;
			}

			if (_httpContextAccessor.HttpContext == null)
			{
				return false;
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Delete("SmartHub-Access-Token");
			_httpContextAccessor.HttpContext.Response.Cookies.Delete("SmartHub-Refresh-Token");
			return true;
		}
	}
}