using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Domain.Entities.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database
{
	public class SeedDatabase
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger = Log.ForContext<SeedDatabase>();

		public SeedDatabase(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
		{
			_scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
			_unitOfWork = unitOfWork;
		}

		public async Task SeedData(bool seedDb)
		{
			if (!seedDb) return;
			_logger.Information($"Start seeding into database ...");
			await SeedRoleData();
			await SeedUserData();
			await _unitOfWork.SaveAsync();
			_logger.Information($"Finished seeding into database.");
		}

		private async Task SeedUserData()
		{
			using var serviceScope = _scopeFactory.CreateScope();
			var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
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

			if (!userManager.Users.Any(x => x.UserName == admin.UserName))
			{
				await userManager.CreateAsync(admin, "Test-123");
				await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
			}
			if (!userManager.Users.Any(x => x.UserName == user.UserName))
			{
				await userManager.CreateAsync(user, "Test-123");
				await userManager.AddToRoleAsync(user, Roles.User.ToString());
			}
			if (!userManager.Users.Any(x => x.UserName == guest.UserName))
			{
				await userManager.CreateAsync(guest, "Test-123");
				await userManager.AddToRoleAsync(guest, Roles.Guest.ToString());
			}
		}

		private async Task SeedRoleData()
		{
			using var serviceScope = _scopeFactory.CreateScope();
			var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
			var admin = new Role(Roles.Admin.ToString(), "Admin description") {CreatedBy = "Home", LastModifiedBy = "Home"};
			var guest = new Role(Roles.Guest.ToString(), "Guest description") {CreatedBy = "Home", LastModifiedBy = "home"};
			var user = new Role(Roles.User.ToString(), "User description") {CreatedBy = "Home", LastModifiedBy = "Home"};

			if (!roleManager.Roles.Any(x => x.Name == admin.Name))
			{
				await roleManager.CreateAsync(admin);
			}
			if (!roleManager.Roles.Any(x => x.Name == user.Name))
			{
				await roleManager.CreateAsync(user);
			}
			if (!roleManager.Roles.Any(x => x.Name == guest.Name))
			{
				await roleManager.CreateAsync(guest);
			}
		}
	}
}
