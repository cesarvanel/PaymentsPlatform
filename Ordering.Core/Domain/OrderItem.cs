using Ordering.Core.Domain.Vo;


namespace Ordering.Core.Domain
{
    public class OrderItem(Product product, Quantity quantity)
    {

        public Quantity Quantity { get; private set; } = quantity;
        public Product Product { get; } = product;

        public OrderItem IncrementQuantity(Quantity quantity) => new (Product, Quantity.Add(quantity));

        public OrderItem DecrementQuantity(Quantity quantity) => new (Product, Quantity.Remove(quantity));

        public Money TotalAmount => new(Quantity.Value * Product.Snapshot.Price , Currency.Xaf);


    }
}
