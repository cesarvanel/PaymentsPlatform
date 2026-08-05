

using Ordering.Core.Domain;
using Ordering.Core.Domain.Snapshot;
using Shared.Domain;
using Shared.Domain.Vo;

namespace Ordering.Infra.Persistence.Models
{
    public class OrderModel:AuditableModel
    {

        public const string TableName = "orders";
        public Guid Id { get; set; }
        public bool IsValid { get; set; }

        public List<OrderItemModel> Items { get; set; } = [];


        public static Order ToDomain(OrderModel orderModel)
        {
            var orderItems = MapItemsToDomain(orderModel.Items);

            return Order.Reconstitute(orderModel.Id, orderItems, orderModel.IsValid);

        }

        public static OrderModel ToPersistence(Order order)
        {
            var snapshot = order.ToSnapshot();

            return new OrderModel
            {
                Id = snapshot.Id,
                IsValid = snapshot.IsValid,
                Items = MapItemsToPersistence(snapshot.Items)
            };
        }

        private static List<OrderItemModel> MapItemsToPersistence(IReadOnlyList<OrderItemSnapshot> items)
        {
            return items
                .Select(item => new OrderItemModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPrice = item.ProductPrice,
                    Currency = item.Currency,
                    Quantity = item.Quantity
                })
                .ToList();
        }

        private static List<OrderItem> MapItemsToDomain(List<OrderItemModel> itemModels)
        {
            return itemModels
                .Select(m => new OrderItem(
                    Product.Create(m.ProductId, m.ProductName, new Money(m.ProductPrice, m.Currency)),
                    new Quantity(m.Quantity)))
                .ToList();
        }
    }

    public class OrderItemModel
    {
        public int Quantity { get; set; }
        public Guid ProductId {  get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }

        public Currency Currency { get; set; }

    }

   
}
