using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Enums;

namespace SmartHub.Infrastructure.Database.Configurations
{
	public class SettingConfiguration : IEntityTypeConfiguration<Setting>
	{
		public void Configure(EntityTypeBuilder<Setting> builder)
		{
			builder.ToTable("Settings");
			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.Name).IsUnique();

			builder.Property(x => x.Type)
				.HasConversion<string>(new EnumToStringConverter<SettingTypes>());
		}
	}
}
