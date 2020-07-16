using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Enums;
using Role = SmartHub.Domain.Enums.Role;

namespace SmartHub.Infrastructure.Database
{
	public class SeedDatabase
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly IUnitOfWork _unitOfWork;
		private UserManager<User> _userManager;
		private RoleManager<Domain.Entities.Role> _roleManager;

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
			var admin = new User("Admin", null, new PersonName("Max", "", "Test"), null)
			{
				CreatedBy = "Home", LastModifiedBy = "Home"
			};
			var guest = new User("Guest", null, new PersonName("Guest", "Person", "Test"), null)
			{
				CreatedBy = "Home", LastModifiedBy = "home"
			};
			var user = new User("User", null, new PersonName("Test", "Middle", "Best"), null)
			{
				CreatedBy = "Home", LastModifiedBy = "Home"
			};

			if (!_userManager.Users.Any(x => x.UserName == admin.UserName))
			{
				await _userManager.CreateAsync(admin, "Test-123");
				await _userManager.AddToRoleAsync(admin, Role.Admin.ToString());
			}
			if (!_userManager.Users.Any(x => x.UserName == user.UserName))
			{
				await _userManager.CreateAsync(user, "Test-123");
				await _userManager.AddToRoleAsync(user, Role.User.ToString());
			}
			if (!_userManager.Users.Any(x => x.UserName == guest.UserName))
			{
				await _userManager.CreateAsync(guest, "Test-123");
				await _userManager.AddToRoleAsync(guest, Role.Guest.ToString());
			}
		}

		private async Task SeedRoleData()
		{
			using var serviceScope = _scopeFactory.CreateScope();
			_roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Domain.Entities.Role>>();
			var admin = new Domain.Entities.Role(Role.Admin.ToString(), "Admin description") {CreatedBy = "Home", LastModifiedBy = "Home"};
			var guest = new Domain.Entities.Role(Role.Guest.ToString(), "Guest description") {CreatedBy = "Home", LastModifiedBy = "home"};
			var user = new Domain.Entities.Role(Role.User.ToString(), "User description") {CreatedBy = "Home", LastModifiedBy = "Home"};

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
