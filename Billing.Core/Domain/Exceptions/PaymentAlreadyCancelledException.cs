using Shared.Domain.Exceptions;

namespace Billing.Core.Domain.Exceptions
{
    public class PaymentAlreadyCancelledException():DomainException("Impossible de canceler un paiement deja cancelé.")
    {
    }
}
