using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Configs
{
	public class GroupConfig : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.ToTable("Groups");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.HasDefaultValueSql("uuid_generate_v4()");

			builder.HasIndex(x => x.Name).IsUnique();

			builder.HasMany(x => x.Devices)
				.WithMany(x => x.Groups)
				.UsingEntity(j => j.ToTable("GroupsDevices"));
		}
	}
}
