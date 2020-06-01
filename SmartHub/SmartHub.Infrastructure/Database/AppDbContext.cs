using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartHub.Domain.Entities.Groups;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Entities.Roles;
using SmartHub.Domain.Entities.Users;
using SmartHub.Infrastructure.Database.Configurations;

namespace SmartHub.Infrastructure.Database
{
	public class AppDbContext : KeyApiAuthorizationDbContext<User, Role, string>
	{
		public AppDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}

		public DbSet<Home> Homes { get; set; }
		public DbSet<Group> Groups { get; set; }
		//public DbSet<Device> Devices { get; set; }
		//public DbSet<Plugin> Plugins { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new HomeConfiguration())
				.ApplyConfiguration(new GroupConfiguration())
				.ApplyConfiguration(new DeviceConfiguration())
				.ApplyConfiguration(new RoleConfiguration())
				.ApplyConfiguration(new UserConfiguration())
				.ApplyConfiguration(new PluginConfiguration())
				.ApplyConfiguration(new SettingConfiguration());
		}
	}
}
