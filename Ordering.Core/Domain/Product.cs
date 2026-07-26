using Shared.Domain.Exceptions;
using Shared.Domain.Vo;

namespace Ordering.Core.Domain
{

    public readonly record struct ProductSnapshot(Guid Id, string Name, decimal Price , Currency Currency);

    public class Product
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly Money _price;

        private Product(Guid id, string name, Money price)
        {

            if(string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException($"Le nom du produit ne peut pas être vide. {nameof(name)}");
            }
            _id = id;
            _name = name;
            _price = price;
        }

        public static Product Create(Guid id , string name, Money price) => new (id, name, price);

        public ProductSnapshot Snapshot => new(_id, _name, _price.Amount, _price.Currency);


    }
}
