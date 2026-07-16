using Ordering.Core.Application.Commands.CreateOrder;
using Ordering.Tests.Units.Builder;
using Ordering.Tests.Units.InMemory;


namespace Ordering.Tests.Units
{
    public class HandleCreateOrderTest
    {

        [Fact]
        public async Task Handle_ReturnsFailure_WhenProductNotExists()
        {
            var orderRepository = new InMemoryOrderRepository();
            var productRepository = new InMemoryProductRepository();

            var handler = new CreateOrderHandler(orderRepository, productRepository);

            var command = new CreateOrderCommandBuilder()
                .WithItems(Guid.NewGuid(), 2)
                .Build();

            var result = await handler.HandleAsync(command, TestContext.Current.CancellationToken);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task Handle_CreatesOrderSuccessfully()
        {

            var productId = Guid.NewGuid();
            var product = new ProductBuilder().WithId(productId).Build();

            var orderRepository = new InMemoryOrderRepository();
            var productRepository = new InMemoryProductRepository();
            productRepository.Initialize(product);

            var handler = new CreateOrderHandler(orderRepository, productRepository);

            var command = new CreateOrderCommandBuilder().WithItems(productId, 2).Build();

            var result = await handler.HandleAsync(command, TestContext.Current.CancellationToken);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.IsCreated);

        }


    }
}
