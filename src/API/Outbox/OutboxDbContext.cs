using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace API.Outbox;

public class OutboxDbContext(DbContextOptions<OutboxDbContext> dbOptions) : DbContext(dbOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}