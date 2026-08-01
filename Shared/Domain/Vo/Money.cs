
using Shared.Domain.Exceptions;

namespace Shared.Domain.Vo
{
    public class Money
    {
        public decimal Value { get; }

        public Currency Currency { get; }

            
        public Money(decimal amount, Currency currency)
        {

            if (amount < 0) throw new NegativeAmountException();
            Value = amount;
            Currency = currency;
        }

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Value + other.Value, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Value - other.Value, Currency);
        }

        public Money WithDiscount(decimal rate)
        {
            decimal discountAmount = Value - (Value * rate);
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
