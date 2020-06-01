using System;
using System.Collections.Generic;

namespace SmartHub.Application.UseCases.Identity
{
	public class AuthResponseDto
	{
		public string Token { get; }

		public DateTime? ExpiresAt { get; set; } // Is inside the token

		public string UserName { get; }
		public List<string> Roles { get; }

		public AuthResponseDto(string token, string username, List<string> roles, DateTime expiresat)
		{
			Token = token;
			UserName = username;
			Roles = roles;
			ExpiresAt = expiresat;
		}
	}
}
