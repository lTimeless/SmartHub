using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Persistence.Database.Configurations
{
	public class GroupConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.ToTable("Groups");
			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.Name).IsUnique();

			builder.HasMany(x => x.Devices)
				.WithOne()
				// .HasForeignKey(x => x.Id)
				;
		}
	}
}
