using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Configs
{
	public class PluginConfig : IEntityTypeConfiguration<Plugin>
	{
		public void Configure(EntityTypeBuilder<Plugin> builder)
		{
			builder.ToTable("Plugins");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.HasDefaultValueSql("uuid_generate_v4()");

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
