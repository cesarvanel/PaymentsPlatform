using Shared.Application.Cqrs.interfaces;

namespace Ordering.Core.Application.Commands.CreateOrder
{
    public record class CreateOrderCommand(List<CreateOrderCommandSubCommand> CreateOrderCommandSubCommand): ICommand<CreateOrderResponse>;

    public record class CreateOrderCommandSubCommand(Guid ProductId, int Quantity);
}
