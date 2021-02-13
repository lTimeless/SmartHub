using Microsoft.AspNetCore.Http;
using SmartHub.Application.Common.Interfaces;
using System.Security.Claims;

namespace SmartHub.WebUI.Services
{
	/// <inheritdoc cref="ICurrentUserService"/>
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		/// <inheritdoc cref="ICurrentUserService.GetCurrentUsername"/>
		public string? GetCurrentUsername()
		{
			return _httpContextAccessor
				.HttpContext?
				.User
				.FindFirstValue(ClaimTypes.Name);
		}
	}
}
