using Shared.Domain.Exceptions;


namespace Billing.Core.Domain.Exceptions
{
    public class PaymentNotFoundException(Guid PaymentId) : DomainException($"Le paiement {PaymentId} n'a pas été trouvé.");
}
