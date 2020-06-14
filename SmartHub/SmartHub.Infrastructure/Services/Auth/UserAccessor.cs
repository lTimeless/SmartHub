using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace SmartHub.Infrastructure.Services.Auth
{
	public class UserAccessor : IUserAccessor
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private const string Home = "Home"; // If no user did a request, than probably the Home itself did it

		public UserAccessor(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetCurrentUsername()
		{
			return _httpContextAccessor
				.HttpContext?
				.User?
				.FindFirstValue(ClaimTypes.NameIdentifier) ?? "null";
		}

		public string GetCurrentUserId()
		{
			return _httpContextAccessor
				.HttpContext?
				.User?
				.FindFirstValue("Id") ?? "null";
		}
	}
}
