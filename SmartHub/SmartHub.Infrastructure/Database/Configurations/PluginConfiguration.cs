using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHub.Domain.Entities.Plugins;
using SmartHub.Domain.Enums;

namespace SmartHub.Infrastructure.Database.Configurations
{
	public class PluginConfiguration : IEntityTypeConfiguration<Plugin>
	{
		public void Configure(EntityTypeBuilder<Plugin> builder)
		{
			builder.ToTable("Plugins");
			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.Name).IsUnique();

			builder.Property(x => x.PluginType)
				.HasConversion<string>(new EnumToStringConverter<PluginTypeEnum>());

			builder.Property(x => x.ConnectionTypeEnum)
				.HasConversion<string>(new EnumToStringConverter<ConnectionTypeEnum>());

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
