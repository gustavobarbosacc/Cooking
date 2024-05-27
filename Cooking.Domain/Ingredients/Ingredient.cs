using Cooking.Domain.Abstractions;
using Cooking.Domain.Products;

namespace Cooking.Domain.Ingredients;

public class Ingredient(
    Guid id,
    Guid userId,
    string name,
    Guid productId,
    Measure measure,
    DateTime CreatedOnUtc) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public Guid ProductId { get; set; } = productId;
    public Measure Measure { get; set; } = measure;

    public DateTime CreatedOnUtc { get; internal set; } = CreatedOnUtc;
    public DateTime? UpdatedOnUtc { get; internal set; }
    public DateTime? RemoveOnUtc { get; internal set; }

    public static Ingredient Create(
        Guid userId,
        string name,
        Product product,
        Measure measure,
        DateTime utcNow)
        => new(
            Guid.NewGuid(),
            userId,
            name,
            product.Id,
            measure,
            utcNow);            

    public Result RemoveOn(DateTime utcNow)
    {
        if (RemoveOnUtc is not null)
        {
            return Result.Failure(IngredientErrors.NotFound);
        }

        RemoveOnUtc = utcNow;

        return Result.Success();
    }
}
