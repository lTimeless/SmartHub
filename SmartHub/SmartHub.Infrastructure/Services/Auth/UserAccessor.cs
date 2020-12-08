using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Infrastructure.Services.Auth
{
	public class UserAccessor : IUserAccessor
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserAccessor(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetCurrentUsername()
		{
			return _httpContextAccessor
				.HttpContext?
				.User
				.FindFirstValue(ClaimTypes.Name) ?? Roles.Anonymous.ToString();
		}
	}
}
