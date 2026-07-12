using Ordering.Core.Domain.Events;
using Ordering.Core.Domain.Exceptions;
using Ordering.Core.Domain.Vo;
using Shared.Domain;

namespace Ordering.Core.Domain
{
    public class Order : AggregateRoot
    {

        private const decimal DiscountRate = 0.05m;
        private const decimal DiscountThreshold = 100_000m;


        private readonly List<OrderItem> _items;
        private bool _isValid;

        public bool IsValid => _isValid;

        public IReadOnlyList<OrderItem> Items => _items;

        private Order(Guid id) : base(id)
        {
            _items = [];
            _isValid = false;

        }

        private Order(Guid id, List<OrderItem> items, bool isValid) : base(id)
        {
            _items = items;
            _isValid = isValid;
        }

        public static Order Create(Guid id) => new(id);

        public static Order Reconstitute(Guid id, IEnumerable<OrderItem> items, bool isValid) => new(id, [.. items], isValid);

        public void AddOrderItem(OrderItem orderItem)
        {

            EnsureIsNotValid();

            int existedItemIndex = _items.FindIndex(item => item.Product.Snapshot.Id == orderItem.Product.Snapshot.Id);
            if (existedItemIndex != -1)
            {
                var item = _items[existedItemIndex];

                item = item.IncrementQuantity(orderItem.Quantity);

                _items[existedItemIndex] = item;
            }
            else
            {
                _items.Add(orderItem);

            }

        }

        public Money Total
        {
            get
            {
                var total = new Money(0m, Currency.Xaf);
                foreach (var item in _items) total = total.Add(item.TotalAmount);
                if (total.Amount > DiscountThreshold) total = total.WithDiscount(DiscountRate);
                return total;
            }
        }


        public void Validate()
        {
            if (Items.Count == 0)
            {
                throw new InvalidOrderException("Impossible de valider une commande sans produit");
            }

            _isValid = true;
            RaiseDomainEvent(new OrderCheckedOut(Id, Total.Amount));

        }


        public IEnumerable<OrderItem> ItemsAboveLing(decimal minLineTotal)
        {
            return Items.Where(item => item.TotalAmount.Amount > minLineTotal);
        }

        private void EnsureIsNotValid()
        {
            if (_isValid)
            {
                throw new InvalidOrderException("Impossible d'ajouter ou de modifier une commande deja verrouillée");
            }
        }

    }
}
