using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities.Roles;
using SmartHub.Domain.Entities.Users;
using SmartHub.Domain.Entities.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;

namespace SmartHub.Infrastructure.Database
{
	public class SeedDatabase
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly IUnitOfWork _unitOfWork;
		private UserManager<User> _userManager;
		private RoleManager<Role> _roleManager;

		public SeedDatabase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
		{
			_scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
			_unitOfWork = unitOfWork;
		}

		public async Task SeedData()
		{
			Log.Information($"[{nameof(SeedData)}] Start seeding into database ...");
			await SeedRoleData();
			await SeedUserData();
			await _unitOfWork.SaveAsync();
			Log.Information($"[{nameof(SeedData)}] Finished seeding into database.");
		}

		private async Task SeedUserData()
		{
			using var serviceScope = _scopeFactory.CreateScope();
			_userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
			var admin = new User("Admin", null, new PersonName("Max", "", "Test"), null);
			var guest = new User("Guest", null, new PersonName("Luci", "", "Test"), null);
			var user = new User("User", null, new PersonName("Test", "Middle", "Best"), null);

			if (!_userManager.Users.Any(x => x.UserName == admin.UserName))
			{
				await _userManager.CreateAsync(admin, "Test-123");
				await _userManager.AddToRoleAsync(admin, "Admin");
			}
			if (!_userManager.Users.Any(x => x.UserName == user.UserName))
			{
				await _userManager.CreateAsync(user, "Test-123");
				await _userManager.AddToRoleAsync(user, "User");
			}
			if (!_userManager.Users.Any(x => x.UserName == guest.UserName))
			{
				await _userManager.CreateAsync(guest, "Test-123");
				await _userManager.AddToRoleAsync(guest, "Guest");
			}
		}

		private async Task SeedRoleData()
		{
			using var serviceScope = _scopeFactory.CreateScope();
			_roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
			var admin = new Role("Admin", "Admin description");
			var guest = new Role("Guest", "Guest description");
			var user = new Role("User", "User description");

			if (!_roleManager.Roles.Any(x => x.Name == admin.Name))
			{
				await _roleManager.CreateAsync(admin);
			}
			if (!_roleManager.Roles.Any(x => x.Name == user.Name))
			{
				await _roleManager.CreateAsync(user);
			}
			if (!_roleManager.Roles.Any(x => x.Name == guest.Name))
			{
				await _roleManager.CreateAsync(guest);
			}
		}
	}
}
