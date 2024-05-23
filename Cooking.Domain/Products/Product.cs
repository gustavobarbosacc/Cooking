using Cooking.Domain.Abstraction;

namespace Cooking.Domain.Products;

public class Product : Entity
{
    public string Name { get; set; }

    public Product(Guid id, string name) : base(id)
    {
        Name = name;
    }
    public static Product Create(string name)
            => new(Guid.NewGuid(), name);
}
