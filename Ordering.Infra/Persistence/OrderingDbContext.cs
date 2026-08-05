using Microsoft.EntityFrameworkCore;
using Ordering.Infra.Persistence.Models;
using Shared.Domain;


namespace Ordering.Infra.Persistence
{
    public class OrderingDbContext(DbContextOptions<OrderingDbContext> options):DbContext(options)
    {

       public DbSet<OrderModel> Orderings => Set<OrderModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);

            modelBuilder.Entity<OrderModel>().HasQueryFilter(o => !o.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableModel>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(ct);
        }
    }
}
