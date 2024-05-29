using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Ingredients;

public class Ingredient(
    Guid id,
    Guid userId,
    Guid productId,
    Measure measure,
    string name,
    double quantity, 
    DateTime CreatedOnUtc) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public Guid ProductId { get; set; } = productId;
    public Measure Measure { get; set; } = measure;
    public string Name { get; set; } = name;
    public double Quantity { get; set; } = quantity;    

public DateTime CreatedOnUtc { get; internal set; } = CreatedOnUtc;
    public DateTime? UpdatedOnUtc { get; internal set; }
    public DateTime? RemoveOnUtc { get; internal set; }

    public static Ingredient Create(Guid userId, Guid productId, Measure measure, string name, double quantity, DateTime utcNow)
        => new(Guid.NewGuid(), userId, productId, measure, name, quantity, utcNow);

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

