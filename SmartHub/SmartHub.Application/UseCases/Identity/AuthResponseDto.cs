using System;
using System.Collections.Generic;
using NodaTime;

namespace SmartHub.Application.UseCases.Identity
{
	public class AuthResponseDto
	{
		public string Token { get; set; }

		public AuthResponseDto(string token)
		{
			Token = token;
		}
	}
}
