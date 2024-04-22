using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;
using Users.Core.Enums;

namespace Users.Core.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.HashedPassword)
            .IsRequired();

        builder.Property(x => x.Username)
            .IsRequired();

        builder.Property(x => x.Role)
            .HasDefaultValue(Role.User);

        builder.Property(x => x.EmailConfirmed)
            .HasDefaultValue(false);
    }
}