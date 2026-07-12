using Shared.Application.Cqrs.interfaces;

namespace Ordering.Core.Application.Commands.CheckoutOrder
{
    public record class CheckoutOrderCommand(Guid OrderId) : ICommand<Guid>;
}
