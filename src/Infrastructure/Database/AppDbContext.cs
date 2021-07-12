using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AppRoles = SmartHub.Domain.Common.Enums.Roles;

namespace SmartHub.Infrastructure.Database
{
	public sealed class AppDbContext : IdentityDbContext<User, Role, string>
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
		public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

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
						entry.Entity.CreatedBy = userName ?? AppRoles.Guest.ToString();
						entry.Entity.LastModifiedBy = userName ?? AppRoles.Guest.ToString();
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedAt = dateTime;
						entry.Entity.LastModifiedBy = userName ?? AppRoles.System.ToString();
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}