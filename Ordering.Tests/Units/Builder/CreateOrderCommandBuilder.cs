using Ordering.Core.Application.Commands.CreateOrder;


namespace Ordering.Tests.Units.Builder
{
    public class CreateOrderCommandBuilder
    {
        private readonly List<CreateOrderCommandSubCommand> _items = [];

        public CreateOrderCommandBuilder WithItems(Guid productId, int quantity)
        {
            _items.Add(new CreateOrderCommandSubCommand(productId, quantity));
            return this;
        }

        public CreateOrderCommand Build() => new (_items);
    }
}

