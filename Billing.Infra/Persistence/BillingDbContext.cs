using Billing.Infra.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;


namespace Billing.Infra.Persistence
{
    public class BillingDbContext(DbContextOptions<BillingDbContext> options):DbContext(options)
    {

        public DbSet<InvoiceModel> Invoices => Set<InvoiceModel>();
        public DbSet<PaymentModel> Payments => Set<PaymentModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof (BillingDbContext).Assembly);

            modelBuilder.Entity<InvoiceModel>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<PaymentModel>().HasQueryFilter(p => !p.IsDeleted);

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
