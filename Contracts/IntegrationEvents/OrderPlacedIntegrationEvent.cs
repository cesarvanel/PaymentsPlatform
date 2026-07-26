
using Shared.Domain.Event;
using Shared.Domain.Vo;

namespace Contracts.IntegrationEvents
{
    public record OrderPlacedIntegrationEvent(Guid OrderId, decimal Amount, Currency Currency, List<OrderPlacedItem> Items) : IIntegrationEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public string KeyRoot => OrderId.ToString();

    }

    public record OrderPlacedItem(Guid ProductId, string Name, int Quantity, decimal UnitPrice, Currency Currency);
}
