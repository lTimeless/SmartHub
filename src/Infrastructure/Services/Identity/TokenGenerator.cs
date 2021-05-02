using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Domain.Common.Options;
using SmartHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartHub.Infrastructure.Services.Identity
{
	/// <summary>
	/// A token / string generator.
	/// </summary>
	public class TokenGenerator
	{
		private readonly JwtOptions _jwtSettings;

		public TokenGenerator(IOptions<JwtOptions> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}

		/// <summary>
		/// Creates a token from the given parameters.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="roles">The roles to user.</param>
		/// <param name="claims">The claims to user.</param>
		/// <returns>A new random token.</returns>
		/// <exception cref="SmartHubException"></exception>
		public string CreateJwtToken(User user, IEnumerable<string> roles, List<Claim> claims)
		{
			if (user == null)
			{
				throw new SmartHubException("Error: The given user is null");
			}
			claims.AddRange(new List<Claim>
			{
				new(ClaimTypes.Name, user.UserName),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // jwt Id
			});
			claims.AddRange(roles.Select(role => new Claim("roles", role)));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key ?? string.Empty));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Audience = _jwtSettings.Audience,
				Issuer = _jwtSettings.Issuer,
				Subject = new(claims),
				Expires = DateTime.Now.AddMinutes(_jwtSettings.LifeTimeInMinutes),
				SigningCredentials = new(key, SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
