using Ordering.Core.Domain.Exceptions;

namespace Ordering.Core.Domain.Vo
{
    public class Money
    {
        public decimal Amount { get; }

        public Currency Currency { get; }


        public Money(decimal amount, Currency currency)
        {

            if (amount < 0) throw new NegativeAmountException();
            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Substract(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount - other.Amount, Currency);
        }

        public Money WithDiscount(decimal rate)
        {
            decimal discountAmount = Amount - (Amount * rate);
            return new Money(discountAmount, Currency);
        }

        public bool IsSameCurrency(Money other) => Currency == other.Currency;

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new CurrencyMismatchException();
            }
        }
    }
}
