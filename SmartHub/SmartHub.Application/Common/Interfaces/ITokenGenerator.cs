using System.Collections.Generic;
using System.Security.Claims;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Interfaces
{
	public interface ITokenGenerator
	{
		/// <summary>
		/// Creates a jwt token based on the given user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns>The generated jwt token.</returns>
		string CreateJwtToken(User user, List<string> roles, List<Claim> claims);
	}
}
