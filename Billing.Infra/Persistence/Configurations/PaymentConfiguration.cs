

using Billing.Infra.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Infra.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<PaymentModel>
    {
        public void Configure(EntityTypeBuilder<PaymentModel> builder)
        {

            builder.ToTable(PaymentModel.TableName);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.InvoiceId).IsRequired();

            builder.Property(p => p.Amount).HasPrecision(18, 2);

            builder.Property(p => p.Currency).HasConversion<string>().HasMaxLength(10);
            builder.Property(p => p.Method).HasConversion<string>().HasMaxLength(20);
            builder.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);

            builder.Property(p => p.PaidAt);

        }
    }
}
