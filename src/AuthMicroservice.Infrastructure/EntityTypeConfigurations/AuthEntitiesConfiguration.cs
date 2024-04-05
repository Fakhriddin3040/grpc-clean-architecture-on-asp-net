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

		builder.OwnsOne(e => e.Contacts);

		builder.Property(e => e.Username)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(e => e.Password)
			.HasMaxLength(100)
			.IsRequired();
	}
}