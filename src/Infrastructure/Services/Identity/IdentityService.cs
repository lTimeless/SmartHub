using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Options;
using SmartHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly JwtOptions _jwtOptions;
		private readonly IBaseRepositoryAsync<RefreshToken> _refreshTokenRepository;
		private readonly RoleManager<Role> _roleManager;
		private readonly SignInManager<User> _signInManager;
		private readonly TokenValidationParameters _tokenValidationParameters;
		private readonly UserManager<User> _userManager;

		public IdentityService(UserManager<User> userManager, RoleManager<Role> roleManager,
			SignInManager<User> signInManager,
			TokenValidationParameters tokenValidationParameters, IOptions<JwtOptions> jwtSettings,
			IBaseRepositoryAsync<RefreshToken> refreshTokenRepository)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_tokenValidationParameters = tokenValidationParameters;
			_refreshTokenRepository = refreshTokenRepository;
			_jwtOptions = jwtSettings.Value;
		}

		public async Task<bool> CreateUserAsync(User user, string pw, string roleName)
		{
			var roleExist = await _roleManager.RoleExistsAsync(roleName);
			if (!roleExist)
			{
				var role = new Role(roleName, $"This is the {roleName} role");
				await _roleManager.CreateAsync(role);
			}

			var result = await _userManager.CreateAsync(user, pw);
			if (result.Succeeded)
			{
				var addedToRole = await _userManager.AddToRoleAsync(user, roleName);
				return addedToRole.Succeeded;
			}

			return false;
		}

		public async Task<bool> UpdateUserAsync(User user)
		{
			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
		{
			return await _userManager.GetRolesAsync(user);
		}

		public async Task<bool> UserChangeRoleAsync(User user, string newRoleName)
		{
			var exists = await _roleManager.RoleExistsAsync(newRoleName);
			if (!exists)
			{
				var role = new Role(newRoleName, $"This is the {newRoleName} role");
				await _roleManager.CreateAsync(role);
			}

			var roles = await _userManager.GetRolesAsync(user);
			var resultRemoved =
				await _userManager.RemoveFromRoleAsync(user, roles[0]); // because the user can only have one role atm
			if (!resultRemoved.Succeeded)
			{
				return false;
			}

			var resultAdd = await _userManager.AddToRoleAsync(user, newRoleName);
			return resultAdd.Succeeded;
		}

		public async Task<bool> LoginAsync(User user, string password)
		{
			// await _signInManager.SignInAsync(user, new AuthenticationProperties {IsPersistent = true});
			var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			return result.Succeeded;
		}

		public async Task<Tuple<string, RefreshToken>> CreateTokensAsync(User user, List<string>? inputRoles = default,
			RefreshToken? storedRefreshToken = default)
		{
			var roles = inputRoles ?? await GetUserRolesAsync(user);
			var claims = await _userManager.GetClaimsAsync(user) as List<Claim>;
			if (user == null)
			{
				throw new SmartHubException("Error: The given user is null");
			}

			claims ??= new();
			claims.AddRange(new List<Claim>
			{
				new(ClaimTypes.Name, user.UserName),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // jwt Id
			});
			claims.AddRange(roles.Select(role => new Claim("roles", role)));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Audience = _jwtOptions.Audience,
				Issuer = _jwtOptions.Issuer,
				Subject = new(claims),
				IssuedAt = DateTime.Now,
				Expires = DateTime.Now.AddMinutes(_jwtOptions.LifeTimeInMinutes),
				SigningCredentials = new(key, SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var jwtToken = tokenHandler.WriteToken(token);

			// path for login or registration
			if (storedRefreshToken is null)
			{
				storedRefreshToken = await _refreshTokenRepository.FindByAsync(x => x.UserId == user.Id);

				if (storedRefreshToken is not null && storedRefreshToken.Used)
				{
					await _refreshTokenRepository.RemoveAsync(storedRefreshToken);
					storedRefreshToken = null;
				}

				if (storedRefreshToken is not null)
				{
					storedRefreshToken.JwtId = token.Id;
					return new(jwtToken, storedRefreshToken);
				}

				storedRefreshToken = new()
				{
					Token = GenerateToken(),
					JwtId = token.Id,
					User = user,
					Used = false,
					CreatedAt = DateTime.Now,
					ExpirationDate = DateTime.Now.AddMonths(_jwtOptions.RefreshTokenLifeTimeInMonths)
				};
				await _refreshTokenRepository.AddAsync(storedRefreshToken);
			}
			else
			{
				storedRefreshToken.JwtId = token.Id;
				storedRefreshToken.Used = true;
			}

			return new(jwtToken, storedRefreshToken);
		}

		public async Task<bool> UsersExistAsync()
		{
			return await _userManager.Users.AnyAsync();
		}

		public async Task<User?> FindByNameAsync(string username)
		{
			return await _userManager.FindByNameAsync(username);
		}

		public async Task<User?> FindByIdAsync(string userId)
		{
			return await _userManager.FindByIdAsync(userId);
		}

		public async Task<Tuple<string, RefreshToken>?> RefreshTokensAsync(string jwt, string refreshToken)
		{
			var validatedToken = GetPrincipalFromToken(jwt);
			if (validatedToken is null)
			{
				// null = not valid or somehow broken jwt (doesn't include expire check)
				return null;
			}

			var jwtExpiryDateUnix =
				long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
			var jwtExpiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
				.AddSeconds(jwtExpiryDateUnix);

			var storedRefreshToken = await _refreshTokenRepository.FindByAsync(x => x.Token == refreshToken);

			// No refreshToken exist
			if (storedRefreshToken is null)
			{
				return null;
			}

			// JwtToken is not yet expired = return given tokens
			if (jwtExpiryDateUtc > DateTime.UtcNow)
			{
				return new(jwt, storedRefreshToken);
			}


			// RefreshToken is expired
			if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
			{
				return null;
			}

			// RefreshToken is invalidated
			if (storedRefreshToken.Invalidated)
			{
				return null;
			}

			// RefreshToken has been used = user needs to login again
			if (storedRefreshToken.Used)
			{
				return null;
			}

			var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
			// RefreshToken does not match to the jwt
			if (storedRefreshToken.JwtId != jti)
			{
				return null;
			}

			// Create new tokens for user
			var (newJwt, newRefreshToken) =
				await CreateTokensAsync(storedRefreshToken.User, default, storedRefreshToken);

			// TODO Maybe use this build in method
			// await _userManager.GenerateUserTokenAsync(user, "Bearer", "accessToken");
			return new(newJwt, storedRefreshToken);
		}

		private ClaimsPrincipal? GetPrincipalFromToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			try
			{
				_tokenValidationParameters.ValidateLifetime = false;
				var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
				_tokenValidationParameters.ValidateLifetime = true;
				return !IsJwtWithValidSecurityAlg(validatedToken) ? null : principal;
			}
			catch (Exception e)
			{
				return null;
			}
		}

		private bool IsJwtWithValidSecurityAlg(SecurityToken token)
		{
			return token is JwtSecurityToken securityToken &&
			       securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				       StringComparison.InvariantCultureIgnoreCase);
		}

		private static string GenerateToken(int size = 32)
		{
			var randomNumber = new byte[size];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
	}
}