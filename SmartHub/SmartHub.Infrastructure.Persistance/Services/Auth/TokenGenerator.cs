using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Services.Auth
{
	public class TokenGenerator : ITokenGenerator
	{
		private readonly JwtSettings _jwtSettings;
		private readonly IDateTimeService _dateTimeService;

		public TokenGenerator(IOptions<JwtSettings> jwtSettings, IDateTimeService dateTimeService)
		{
			_jwtSettings = jwtSettings.Value;
			_dateTimeService = dateTimeService;
		}

		public string CreateJwtToken(User user, List<string> roles, List<Claim> claims)
		{
			if (user == null)
			{
				throw new SmartHubException(nameof(CreateJwtToken), "The given user is null");
			}
			claims.AddRange(new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			});
			claims.AddRange(roles.Select(role => new Claim("roles", role)));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Audience = _jwtSettings.Audience,
				Issuer = _jwtSettings.Issuer,
				Subject = new ClaimsIdentity(claims),
				Expires = _dateTimeService.Now.ToDateTimeUtc().AddMinutes(_jwtSettings.LifeTimeInMinutes),
				SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
