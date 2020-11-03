using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Configs
{
	public class ConfigurationConfig : IEntityTypeConfiguration<Configuration>
	{
		public void Configure(EntityTypeBuilder<Configuration> builder)
		{
			builder.ToTable("Configurations");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.HasDefaultValueSql("uuid_generate_v4()");

			builder.HasIndex(x => x.Name).IsUnique();

			builder.Property(x => x.Type)
				.HasConversion<string>(new EnumToStringConverter<ConfigurationTypes>());
		}
	}
}
