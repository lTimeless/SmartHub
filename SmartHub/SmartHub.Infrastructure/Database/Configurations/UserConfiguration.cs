using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public UserConfiguration()
		{
		}

		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("AspNetUsers");
			builder.HasKey(x => x.Id);

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
