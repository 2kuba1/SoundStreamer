using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Services;
using Users.Core.Entities;

namespace Users.Core.Database;

public class UserDbContext(DbContextOptions<UserDbContext> options, ICurrentUserService currentUserService)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}