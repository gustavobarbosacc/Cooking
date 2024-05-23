using Cooking.Domain.Abstractions;
using Cooking.Domain.Products;

namespace Cooking.Domain.Ingredients;

public class Ingredient(Guid id, string name, Guid productId, Measure measure) : Entity(id)
{
    public string Name { get; set; } = name;
    public Guid ProductId { get; set; } = productId;
    public Measure Measure { get; set; } = measure;

    public static Ingredient Create(
        string name,
        Product product,
        Measure measure)
        => new(
            Guid.NewGuid(),
            name,
            product.Id,
            measure);   
}
