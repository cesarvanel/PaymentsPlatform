using Ordering.Core.Domain;
using Ordering.Core.Domain.Vo;

namespace Ordering.Tests.Units.Builder
{
    public class ProductBuilder
    {
        private Guid _id = Guid.NewGuid();

        private string _name = "Riz";

        private Money _price = new (15_000m, Currency.Xaf);

        public ProductBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ProductBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ProductBuilder WithPrice(decimal p)
        {
            _price = new Money(p, _price.Currency);   
            return this;
        }

        public Product Build() => Product.Create(_id , _name, _price);

    }
}
