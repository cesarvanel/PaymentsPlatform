using Shared.Domain.Exceptions;

namespace Billing.Core.Domain.Exceptions
{
    public class PaymentExceedsRemainingException()
      : DomainException("Le paiement dépasse le montant restant à payer.");
}
