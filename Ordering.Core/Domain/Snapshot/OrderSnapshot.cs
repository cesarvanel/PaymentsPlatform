using Shared.Domain.Vo;

namespace Ordering.Core.Domain.Snapshot
{
    public record OrderSnapshot(
    Guid Id,
    bool IsValid,
    IReadOnlyList<OrderItemSnapshot> Items);

    public record OrderItemSnapshot(
    Guid ProductId,
    string ProductName,
    decimal ProductPrice,
    Currency Currency,
    int Quantity);
}
