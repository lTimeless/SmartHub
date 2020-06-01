using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities.Roles;

namespace SmartHub.Infrastructure.Database.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("AspNetRoles");
			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.Name).IsUnique();
		}
	}
}
