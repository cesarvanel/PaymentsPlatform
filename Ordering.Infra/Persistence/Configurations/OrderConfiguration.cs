using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Infra.Persistence.Models;

namespace Ordering.Infra.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderModel>
    {

        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.ToTable(OrderModel.TableName);
            builder.HasKey(o => o.Id);

            builder.Property(o => o.IsValid);


            builder.Property(o => o.CreatedAt);
            builder.Property(o => o.UpdatedAt);


            builder.OwnsMany(o => o.Items, items =>
            {
                items.ToJson();
                items.Property(x => x.Quantity);
                items.Property(x => x.ProductId);
                items.Property(x => x.ProductName);
                items.Property(x => x.ProductPrice).HasPrecision(18, 2);
                items.Property(x => x.Currency).HasConversion<string>();
            });
        }
    }
}
