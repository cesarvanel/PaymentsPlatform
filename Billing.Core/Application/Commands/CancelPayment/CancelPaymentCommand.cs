using Shared.Application.Cqrs.interfaces;


namespace Billing.Core.Application.Commands.CancelPayment
{
     public sealed record CancelPaymentCommand(Guid InvoiceId, Guid PaymentId):ICommand<CancelPaymentResponse>
    {
    }
}
