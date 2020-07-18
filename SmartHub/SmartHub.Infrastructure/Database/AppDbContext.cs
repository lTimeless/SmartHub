using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHub.Domain.Entities;
using SmartHub.Infrastructure.Database.Configurations;

namespace SmartHub.Infrastructure.Database
{
	public class AppDbContext : IdentityDbContext<User, Role, string>
	{
		public AppDbContext(DbContextOptions options) : base(options)
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
