using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Products;

public class Product(Guid id, string name) : Entity(id)
{
    public string Name { get; set; } = name;

    public static Product Create(string name)
            => new(Guid.NewGuid(), name);
}
