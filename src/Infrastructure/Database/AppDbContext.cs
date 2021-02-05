using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Entities;
using SmartHub.Infrastructure.Database.Configs;
using System.Reflection;

namespace SmartHub.Infrastructure.Database
{
	public sealed class AppDbContext : IdentityDbContext<User, Role, string>, IAppDbContext
	{
		private readonly ICurrentUserService _currentUserService;

		public AppDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
		{
			_currentUserService = currentUserService;
		}

		public DbSet<Group> Groups { get; set; } = default!;
		public DbSet<Device> Devices { get; set; } = default!;
		public DbSet<Plugin> Plugins { get; set; } = default!;
		public DbSet<Activity> Activities { get; set; } = default!;

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
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
		{
			// TODO: maybe this doesn't work for User and Role
			foreach (var entry in ChangeTracker.Entries<IEntity>())
			{
				var dateTime = DateTimeOffset.Now;
				var userName = _currentUserService.GetCurrentUsername();
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
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
