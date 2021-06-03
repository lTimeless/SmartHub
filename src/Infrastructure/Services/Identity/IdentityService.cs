using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly RoleManager<Role> _roleManager;
		private readonly SignInManager<User> _signInManager;
		private readonly TokenGenerator _tokenGenerator;
		private readonly UserManager<User> _userManager;

		public IdentityService(UserManager<User> userManager, RoleManager<Role> roleManager,
			TokenGenerator tokenGenerator, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_tokenGenerator = tokenGenerator;
			_signInManager = signInManager;
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

		public async Task<string> CreateAccessTokenAsync(User user, List<string>? inputRoles = default)
		{
			var roles = inputRoles ?? await GetUserRolesAsync(user);
			var claims = await _userManager.GetClaimsAsync(user) as List<Claim>;
			return _tokenGenerator.CreateJwtToken(user, roles.ToList(), claims ?? new List<Claim>());
		}

		public async Task<string> CreateRefreshTokenAsync(User user, List<string>? inputRoles = default)
		{
			// TODO implement
			var roles = inputRoles ?? await GetUserRolesAsync(user);
			var claims = await _userManager.GetClaimsAsync(user) as List<Claim>;
			return _tokenGenerator.CreateJwtToken(user, roles.ToList(), claims ?? new List<Claim>());
		}

		public async Task<bool> LoginAsync(User user, string password)
		{
			var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			return result.Succeeded;
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

		public async Task<User> LoginByTokens()
		{
			// TODO hier eine function welche den user auto login macht, wenn mindestens der refreshToken in den cookies ist
			return null;
		}
	}
}