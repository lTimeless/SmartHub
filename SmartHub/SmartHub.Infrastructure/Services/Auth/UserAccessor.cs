using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace SmartHub.Infrastructure.Services.Auth
{
	public class UserAccessor : IUserAccessor
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private const string System = "System"; // If no user did a request, than probably the System itself did it

		public UserAccessor(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetCurrentUsername() =>
			_httpContextAccessor
				.HttpContext?
				.User?
				.FindFirstValue(ClaimTypes.NameIdentifier) ?? System;
	}
}
