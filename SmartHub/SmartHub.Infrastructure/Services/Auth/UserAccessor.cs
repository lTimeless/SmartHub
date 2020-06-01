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
			if ((_httpContextAccessor.HttpContext is null) || (_httpContextAccessor.HttpContext.User is null))
			{
				return Home;
			}
			return _httpContextAccessor
				.HttpContext
				.User
				.Claims?
				.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
				.Value ?? Home;
		}
	}
}
