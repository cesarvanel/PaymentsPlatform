using Contracts.IntegrationEvents;
using Shared.Domain.Vo;

namespace Billing.Tests.Units.Builders
{
    public class OrderPlacedItemBuilder
    {
        private Guid _productId = Guid.NewGuid();
        private string _name = "Sac de riz";
        private int _quantity = 10;
        private decimal _unitPrice = 15000.00m;
        private Currency _currency = Currency.Xaf;


        public OrderPlacedItemBuilder WithProductId(Guid productId)
        {
            _productId = productId;
            return this;
        }

        public OrderPlacedItemBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public OrderPlacedItemBuilder WithQuantity(int quantity)
        {
            _quantity = quantity;
            return this;
        }

        public OrderPlacedItemBuilder WithUnitPrice(decimal unitPrice)
        {
            _unitPrice = unitPrice;
            return this;
        }

        public OrderPlacedItemBuilder WithCurrency(Currency currency)
        {
            _currency = currency;
            return this;
        }

        public OrderPlacedItem Build() => new (_productId, _name, _quantity, _unitPrice, _currency);


        public List<OrderPlacedItem> BuildMany(int number)
        {
            List<OrderPlacedItem> orderPlacedItems = [];
            for (int i =1; i <= number ; i++)
            {
                var builder = new OrderPlacedItemBuilder();
                builder.WithProductId(Guid.NewGuid());
                builder.WithName(i.ToString());
                builder.WithQuantity(_quantity +i +1);
                builder.WithUnitPrice(_unitPrice + 5000.00m);

                orderPlacedItems.Add(builder.Build());
            }

            return orderPlacedItems;

        }
    }
}
