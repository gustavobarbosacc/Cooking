using Cooking.Domain.Ingredients;

namespace Cooking.Application.Ingredients.Find;

public class IngredientResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid ProductId { get; init; }
    public Measure measure { get; init; }
    public double Quantity { get; set; }
    public DateTime CreatedOnUtc { get; init; }
}
