using Ordering.Core.Application.Ports.Out;
using Shared.Application.Cqrs.interfaces;
using Shared.Application.Messaging;


namespace Ordering.Core.Application.Commands.CheckoutOrder
{
    public sealed class CheckoutOrderHandler(IOrderRepository orderRepository) : ICommandHandler<CheckoutOrderCommand, Guid>
    {
        public async Task<Result<Guid>> HandleAsync(CheckoutOrderCommand command, CancellationToken ct = default)
        {
            var order = await orderRepository.GetByIdAsync(command.OrderId, ct);

            if (order is null) return Result<Guid>.Failure("Commande introuvable");

            order.Validate();

            await orderRepository.SaveAsync(order, ct);

            return Result<Guid>.Success(order.Id);
        }
    }
}
