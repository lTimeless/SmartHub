using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
        	_userManager = userManager;
        	_roleManager = roleManager;
        }

        public async Task<bool> CreateUser(User user, string pw, string roleName)
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

        public async Task<bool> UpdateUser(User user)
        {
	        var result = await _userManager.UpdateAsync(user);
	        return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetUserRoles(User user)
        {
	        return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> UserChangeRole(User user, string newRoleName)
        {
        	var exists = await _roleManager.RoleExistsAsync(newRoleName);
            if (!exists)
            {
	            var role = new Role(newRoleName, $"This is the {newRoleName} role");
	            await _roleManager.CreateAsync(role);
            }
        	var roles = await _userManager.GetRolesAsync(user);
        	var resultRemoved = await _userManager.RemoveFromRoleAsync(user, roles[0]); // because the user can only have one role atm
        	if (!resultRemoved.Succeeded)
        	{
        		return false;
        	}
        	var resultAdd = await _userManager.AddToRoleAsync(user, newRoleName);
        	return resultAdd.Succeeded;
        }

        public async Task<bool> UsersExist()
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
    }
}