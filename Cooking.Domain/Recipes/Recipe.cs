using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;
using Cooking.Domain.Ingredients;

namespace Cooking.Domain.Recipes;

public class Recipe(
    Guid id,
    Guid userId,
    Guid categoryId,
    string name,
    string preparationMethod,
    int levels,
    Rating rating,
    int preparationTime,
    List<Ingredient> ingredients) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public Guid CategoryId { get; set; } = categoryId;
    public string Name { get; set; } = name;
    public string PreparationMethod { get; set; } = preparationMethod;
    public int Levels { get; set; } = levels;
    public List<Ingredient> Ingredients { get; set; } = ingredients;
    public Rating Rating { get; set; } = rating;
    public int PreparationTime { get; set; } = preparationTime;

    public static Recipe Create(
        Guid userId,
        string name,
        string preparationMethod,
        int levels,
        List<Ingredient> ingredients,
        Category category,
        Rating rating,
        int preparationTime) => new (
            Guid.NewGuid(),
            userId,
            category.Id,
            name,
            preparationMethod,
            levels,
            rating,
            preparationTime,
            ingredients);
}
