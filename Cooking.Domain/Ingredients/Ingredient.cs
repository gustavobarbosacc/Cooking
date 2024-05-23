using Cooking.Domain.Abstractions;
using Cooking.Domain.Products;

namespace Cooking.Domain.Ingredients;

public class Ingredient(
    Guid id,
    Guid userId,
    string name,
    Guid productId,
    Measure measure) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public Guid ProductId { get; set; } = productId;
    public Measure Measure { get; set; } = measure;

    public static Ingredient Create(
        Guid userId,
        string name,
        Product product,
        Measure measure)
        => new(
            Guid.NewGuid(),
            userId,
            name,
            product.Id,
            measure);   
}
