

namespace Shared.Domain.Exceptions
{
    public class CurrencyMismatchException() : DomainException("La devise de la commande ne correspond pas à la devise du paiement.");

}
