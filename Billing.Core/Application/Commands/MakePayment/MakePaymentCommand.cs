using Billing.Core.Domain.Enum;
using Shared.Application.Cqrs.interfaces;
using Shared.Domain.Vo;

namespace Billing.Core.Application.Commands.MakePayment;

public sealed record MakePaymentCommand(
    Guid InvoiceId,
    decimal Amount,
    Currency Currency,
    PaymentMethod PaymentMethod
) : ICommand<Guid>;