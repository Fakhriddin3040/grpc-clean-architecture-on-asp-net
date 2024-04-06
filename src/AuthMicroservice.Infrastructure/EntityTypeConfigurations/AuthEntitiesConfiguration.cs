using System.ComponentModel;
using AuthMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthMicroservice.Infrastructure.EntityTypeConfigurations;

public class AuthEntitiesConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(e => e.Id);

		builder.OwnsOne(e => e.Contacts, contacts =>
		{
			contacts.Property(e => e.Email)
				.HasMaxLength(38)
				.IsRequired(false);

			contacts.Property(e => e.Phone)
				.HasMaxLength(14)
				.IsRequired(false);
		});

		builder.Property(e => e.FirstName)
			.HasMaxLength(30)
			.IsRequired(false);

		builder.Property(e => e.LastName)
			.HasMaxLength(30)
			.IsRequired(false);

		builder.Property(e => e.IsActive)
			.HasDefaultValue(value: true);

		builder.Property(e => e.Age)
			.HasMaxLength(80)
			.IsRequired(false);

		builder.Property(e => e.CreatedAt)
			.ValueGeneratedOnAdd()
			.HasDefaultValueSql("datetime('now')");

		builder.Property(e => e.UpdatedAt)
			.ValueGeneratedOnUpdate()
			.HasDefaultValueSql("datetime('now')");

		builder.HasIndex(e => e.Username)
			.IsUnique();

		builder.Property(e => e.Username)
			.HasMaxLength(20)
			.IsRequired();

		builder.Property(e => e.Password)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(e => e.Role)
			.IsRequired(false);

		builder.Property(e => e.Birthday)
			.IsRequired(false);
	}
}