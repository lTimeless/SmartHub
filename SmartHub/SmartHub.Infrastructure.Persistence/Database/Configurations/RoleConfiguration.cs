using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Persistence.Database.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles");
			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.Name).IsUnique();
		}
	}
}
