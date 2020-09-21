using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Persistence.Database.Configurations
{
	public class DeviceConfiguration : IEntityTypeConfiguration<Device>
	{
		public DeviceConfiguration()
		{
		}

		public void Configure(EntityTypeBuilder<Device> builder)
		{
			builder.ToTable("Devices");
			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.Name).IsUnique();

			builder.OwnsOne(x => x.Ip, c =>
			{
				c.Property(v => v.Ipv4).HasMaxLength(15)
				.HasDefaultValue("0.0.0.0");
			});

			builder.OwnsOne(x => x.Company, c =>
			{
				c.Property(v => v.Name).HasMaxLength(200)
				.HasDefaultValue("");
				c.Property(v => v.ShortName).HasMaxLength(4)
				.HasDefaultValue("");
			});

			builder.Property(x => x.PrimaryConnection)
				.HasConversion<string>(new EnumToStringConverter<ConnectionTypes>());
			builder.Property(x => x.SecondaryConnection)
				.HasConversion<string>(new EnumToStringConverter<ConnectionTypes>());

			builder.Property(x => x.PluginName);

			builder.Property(x => x.PluginTypes)
				.HasConversion<string>(new EnumToStringConverter<PluginTypes>());
		}
	}
}
