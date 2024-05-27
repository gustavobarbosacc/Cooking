namespace Cooking.Application.Recipes.Find;

public class RecipeResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid CategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string PreparationMethod { get; init; } = string.Empty;
    public int Level { get; init; } = string.Empty.Length;
    public DateTime CreatedOnUtc { get; init; }
}