using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;
using SmartHub.Infrastructure.Database.Configs;

namespace SmartHub.Infrastructure.Database
{
	public sealed class AppDbContext : IdentityDbContext<User, Role, string>
	{
		private readonly IUserAccessor _userAccessor;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		public AppDbContext(DbContextOptions options, IUserAccessor userAccessor) : base(options)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			_userAccessor = userAccessor;
		}

		//public DbSet<Home> Homes { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<Plugin> Plugins { get; set; }
		public DbSet<Activity> Activities { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Naming the schema
			builder.HasDefaultSchema("smarthub");
			// Set Extension for autogenerate the Id property
			builder.HasPostgresExtension("uuid-ossp");
			// Rename alle Identity Tables
			builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
			builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

			// Apply entity configurations
			builder
				//.ApplyConfiguration(new HomeConfig())
				.ApplyConfiguration(new GroupConfig())
				.ApplyConfiguration(new DeviceConfig())
				.ApplyConfiguration(new RoleConfig())
				.ApplyConfiguration(new UserConfig())
				.ApplyConfiguration(new PluginConfig())
				.ApplyConfiguration(new ActivityConfig());
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			// TODO: maybe this doesn't work for User and Role
			foreach (var entry in ChangeTracker.Entries<IEntity>())
			{
				var dateTime = DateTimeOffset.Now;
				var userName = _userAccessor.GetCurrentUsername();
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedAt = dateTime;
						entry.Entity.LastModifiedAt = dateTime;
						entry.Entity.CreatedBy = userName;
						entry.Entity.LastModifiedBy = userName;
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedAt = dateTime;
						entry.Entity.LastModifiedBy = userName;
						break;
					case EntityState.Detached:
						break;
					case EntityState.Unchanged:
						break;
					case EntityState.Deleted:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
