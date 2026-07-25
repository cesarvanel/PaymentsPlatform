using Shared.Domain.Exceptions;

namespace Billing.Core.Domain.Exceptions
{
    public class PaymentNotAllowedException(string? message):DomainException(message ?? "Impossible de payer une facture  annulée ou deja payée .")
    {
    }
}
