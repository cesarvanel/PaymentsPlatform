using Ordering.Core.Application.Commands.CheckoutOrder;
using Ordering.Core.Domain;
using Ordering.Core.Domain.Vo;
using Ordering.Tests.Units.InMemory;

namespace Ordering.Tests.Units
{
    public class CheckoutOrderHandlerTests
    {

        [Fact]
        public async Task Handle_ReturnsFailure_WhenOrderNotFound()
        {
            var handler = new CheckoutOrderHandler(new InMemoryOrderRepository());

            var result = await handler.HandleAsync(new CheckoutOrderCommand(Guid.NewGuid()), TestContext.Current.CancellationToken);
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public async Task Handle_ValidatesAndSave_WhenOrderExists()
        {
            var orderRepository = new InMemoryOrderRepository();

            var order = OrderWithItem();

            orderRepository.Initialize(order);

            var handler = new CheckoutOrderHandler(orderRepository);

            var result = await handler.HandleAsync(new CheckoutOrderCommand(order.Id), TestContext.Current.CancellationToken);

            Assert.True(result.IsSuccess);
            Assert.Equal(order.Id, result.Value);
            Assert.True(order.IsValid);
            Assert.Single(order.DomainEvents);
        }



        private static Order OrderWithItem()
        {
            var order = Order.Create(Guid.NewGuid());
            var orderItem = new OrderItem(Product.Create(Guid.NewGuid(), "Riz", new Money(15_000m, Currency.Xaf)), new Quantity(2));
            order.AddOrderItem(orderItem);
            return order;
        }


    }
}
