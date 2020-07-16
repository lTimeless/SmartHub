using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using System.Linq;
using System.Security.Claims;
using SmartHub.Domain.Enums;

namespace SmartHub.Infrastructure.Services.Auth
{
	public class UserAccessor : IUserAccessor
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserAccessor(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetCurrentUsername() =>
			_httpContextAccessor
				.HttpContext?
				.User?
				.FindFirstValue(ClaimTypes.NameIdentifier) ?? Role.System.ToString();
	}
}
