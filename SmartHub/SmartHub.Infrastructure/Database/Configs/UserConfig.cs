using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Configs
{
	public class UserConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.HasDefaultValueSql("uuid_generate_v4()");

			builder.HasIndex(x => x.UserName).IsUnique();

			builder.OwnsOne(m => m.PersonName, a =>
			{
				a.Property(x => x.FirstName).HasMaxLength(100)
					.HasDefaultValue("");
				a.Property(x => x.MiddleName).HasMaxLength(100)
					.HasDefaultValue("");
				a.Property(x => x.LastName).HasMaxLength(200)
					.HasDefaultValue("");
			});
		}
	}
}
