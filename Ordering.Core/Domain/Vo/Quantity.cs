
namespace Ordering.Core.Domain.Vo
{
    public class Quantity
    {
        public int Value { get;}

        public Quantity(int value)
        {
            if(value < 1)
            {
                throw new Exceptions.QuantityValueException();
            }
            Value = value;
        }

        public Quantity Add(Quantity quantity) => new (Value + quantity.Value);

        public Quantity Remove(Quantity quantity) => new(Value - quantity.Value);

    }
}
