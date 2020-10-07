using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Persistence.Database.Configurations
{
	public class PluginConfiguration : IEntityTypeConfiguration<Plugin>
	{
		public void Configure(EntityTypeBuilder<Plugin> builder)
		{
			builder.ToTable("Plugins");
			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.Name).IsUnique();

			builder.Property(x => x.PluginTypes)
				.HasConversion<string>(new EnumToStringConverter<PluginTypes>());

			builder.Property(x => x.ConnectionTypes)
				.HasConversion<string>(new EnumToStringConverter<ConnectionTypes>());

			builder.OwnsOne(x => x.Company, c =>
			{
				c.Property(v => v.Name).HasMaxLength(200)
				.HasDefaultValue("");
				c.Property(v => v.ShortName).HasMaxLength(4)
				.HasDefaultValue("");
			});
		}
	}
}
