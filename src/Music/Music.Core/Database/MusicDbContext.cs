using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Services;

namespace Music.Core.Database;

public class MusicDbContext(DbContextOptions<MusicDbContext> options, ICurrentUserService currentUserService)
    : DbContext(options)
{
    public DbSet<Entities.Music> Musics => Set<Entities.Music>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.LastModifiedAt = DateTime.UtcNow;
            entry.Entity.LatModifiedBy = currentUserService.Id;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = currentUserService.Id;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}