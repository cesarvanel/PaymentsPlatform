

using Billing.Infra.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Infra.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<InvoiceModel>
    {
        public void Configure(EntityTypeBuilder<InvoiceModel> builder)
        {
            builder.ToTable(InvoiceModel.TableName);

            builder.HasKey(i => i.Id);

            builder.Property(i => i.OrderId).IsRequired();
            builder.HasIndex(i => i.OrderId);

            builder.Property(i => i.TotalAmount).IsRequired().HasPrecision(18, 2);

            builder.Property(i => i.Currency).HasConversion<string>().HasMaxLength(10);

            builder.Property(i => i.State).HasConversion<string>().HasMaxLength(20);

            builder.Property(i => i.CreatedAt);
            builder.Property(i => i.UpdatedAt);


            builder.OwnsMany(i => i.Items, items =>
            {
                items.ToJson();
                items.Property(x => x.Quantity);
                items.Property(x => x.ProductId);
                items.Property(x => x.ProductName);
                items.Property(x => x.ProductPrice).HasPrecision(18, 2);
                items.Property(x => x.Currency).HasConversion<string>();
            });

            builder.HasMany(i => i.Payments)
                   .WithOne()
                   .HasForeignKey(p => p.InvoiceId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
