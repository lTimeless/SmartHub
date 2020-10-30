using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Configurations
{
	public class HomeConfiguration : IEntityTypeConfiguration<Home>
	{
		public void Configure(EntityTypeBuilder<Home> builder)
		{
			builder.ToTable("Homes");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.HasDefaultValueSql("uuid_generate_v4()");

			builder.HasIndex(x => x.Name).IsUnique();

			builder.OwnsOne(x => x.Address, c =>
			{
				c.Property(v => v!.Street).HasMaxLength(200)
				.HasDefaultValue("");
				c.Property(v => v!.City).HasMaxLength(100)
				.HasDefaultValue("");
				c.Property(v => v!.State).HasMaxLength(100)
				.HasDefaultValue("");
				c.Property(v => v!.Country).HasMaxLength(100)
				.HasDefaultValue("");
				c.Property(v => v!.ZipCode).HasMaxLength(20)
				.HasDefaultValue("");
			});

			builder.Ignore(x => x.Events);

			builder.HasMany(x => x.Users)
				.WithOne(x => x.Home!)
				.HasForeignKey(x => x.HomeId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.Groups)
				.WithOne()
				// .HasForeignKey(x => x.HomeId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.Plugins)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.Settings)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(x => x.Activities)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
