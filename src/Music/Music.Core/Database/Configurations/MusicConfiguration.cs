using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Music.Core.Database.Configurations;

public class MusicConfiguration : IEntityTypeConfiguration<Entities.Music>
{
    public void Configure(EntityTypeBuilder<Entities.Music> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.FileUrl)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired();
    }
}