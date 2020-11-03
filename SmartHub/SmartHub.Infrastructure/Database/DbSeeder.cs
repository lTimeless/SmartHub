using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Infrastructure.Database
{
	/// <inheritdoc cref="IDbSeeder"/>
	public class DbSeeder : IDbSeeder
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger = Log.ForContext(typeof(DbSeeder));
		private readonly List<User> _userList = new List<User>();

		public DbSeeder(IUnitOfWork unitOfWork, IServiceScopeFactory scopeFactory)
		{
			_unitOfWork = unitOfWork;
			_scopeFactory = scopeFactory;
		}

		/// <inheritdoc cref="IDbSeeder.SeedData"/>
		public async Task SeedData()
		{
			_logger.Information("Start seeding database ...");
			await SeedRoleData().ConfigureAwait(false);
			await SeedUserData().ConfigureAwait(false);
			await SeedSmartHubTestData().ConfigureAwait(false);
			await _unitOfWork.SaveAsync();
			_logger.Information("Finished seeding database.");
		}

		private async Task SeedUserData()
		{
			using var serviceScope = _scopeFactory.CreateScope();
			var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
			var admin = new User("Admin", "", new PersonName("Max", "", "Test"))
			{
				CreatedBy = "Home", LastModifiedBy = "Home"
			};
			var guest = new User("Guest", "", new PersonName("Guest", "Person", "Test"))
			{
				CreatedBy = "Home", LastModifiedBy = "home"
			};
			var user = new User("User", "", new PersonName("Test", "Middle", "Best"))
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

			_userList.AddRange(new []{ admin, user, guest });
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

		private async Task SeedSmartHubTestData()
		{
			var fakeCompany = new Faker<Company>()
				.RuleFor(x => x.Name, c => c.Company.CompanyName())
				.RuleFor(x => x.ShortName, c => c.Company.CompanyName().PadLeft(4));

			var fakeIp = new Faker<IpAddress>()
				.RuleFor(x => x.Ipv4, c => c.Internet.Ip());

			var fakeDevices = new Faker<Device>()
					.RuleFor(x => x.Company, _ => fakeCompany.Generate())
					.RuleFor(x => x.Name, c => c.Lorem.Word())
					.RuleFor(x => x.Ip, _ => fakeIp.Generate())
					.RuleFor(x => x.PluginTypes, c => c.PickRandom<PluginTypes>())
					.RuleFor(x => x.PrimaryConnection, c => c.PickRandom<ConnectionTypes>())
					.RuleFor(x => x.SecondaryConnection, c => c.PickRandom<ConnectionTypes>())
					.RuleFor(x => x.PluginName, c => c.Lorem.Word());

			var fakeSubgroups = new Faker<Group>()
					.RuleFor(x => x.Name, c => c.Lorem.Word())
					.RuleFor(x => x.Devices, _ => fakeDevices.Generate(3).ToList())
					.RuleFor(x => x.IsSubGroup, _ => true);

			var fakeGroups = new Faker<Group>()
					.RuleFor(x => x.Name, c => c.Lorem.Word())
					.RuleFor(x => x.Devices, _ => fakeDevices.Generate(3).ToList())
					.RuleFor(x => x.SubGroups, _ => fakeSubgroups.Generate(2).ToList());

			var fakeSettings = new Faker<Configuration>()
					.RuleFor(x => x.Name, c => c.Lorem.Word())
					.RuleFor(x => x.Type,  c => c.PickRandom<ConfigurationTypes>())
					.RuleFor(x => x.IsActive, c => c.Random.Bool())
					.RuleFor(x => x.IsDefault, c => c.Random.Bool())
					.RuleFor(x => x.Description, c => c.Lorem.Sentence());

			var fakeActivities = new Faker<Activity>()
					.RuleFor(x => x.Username, c => c.Person.FirstName)
					.RuleFor(x => x.Name, c => c.Lorem.Word())
					.RuleFor(x => x.SuccessfulRequest, c => c.Random.Bool())
					.RuleFor(x => x.DateTime, c => c.Date.Recent().ToString(CultureInfo.CurrentCulture))
					.RuleFor(x => x.Message, c => c.Lorem.Sentence())
					.RuleFor(x => x.ExecutionTime, c => c.Random.Decimal());

			var fakeAddress = new Faker<Address>()
					.RuleFor(x => x.City, c => c.Address.City())
					.RuleFor(x => x.Country, c => c.Address.Country())
					.RuleFor(x => x.State, c => c.Address.State())
					.RuleFor(x => x.Street, c => c.Address.StreetName())
					.RuleFor(x => x.ZipCode, c => c.Address.ZipCode());

			var fakeHome = new Faker<Home>()
					.RuleFor(x => x.Address, _ => fakeAddress.Generate())
					.RuleFor(x => x.Activities, _ => fakeActivities.Generate(2).ToList())
					.RuleFor(x => x.Groups, _ => fakeGroups.Generate(2).ToList())
					.RuleFor(x => x.Settings, _ => fakeSettings.Generate(2).ToList())
					.RuleFor(x => x.Name, _ => "SmartHub")
					.RuleFor(x => x.Users, _ => _userList);

			var home = fakeHome.Generate();
			await _unitOfWork.HomeRepository.AddAsync(home);
		}
	}
}
