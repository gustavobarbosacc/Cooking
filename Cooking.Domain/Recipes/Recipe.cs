using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;
using Cooking.Domain.Ingredients;

namespace Cooking.Domain.Recipes;

public class Recipe(
    Guid id,
    string name,
    string preparationMethod,
    int levels,
    List<Ingredient> ingredients,
    Guid categoryId,
    Rating rating,
    int preparationTime) : Entity(id)
{
    public string Name { get; set; } = name;
    public string PreparationMethod { get; set; } = preparationMethod;
    public int Levels { get; set; } = levels;
    public List<Ingredient> Ingredients { get; set; } = ingredients;
    public Guid CategoryId { get; set; } = categoryId;
    public Rating Rating { get; set; } = rating;
    public int PreparationTime { get; set; } = preparationTime;

    public static Recipe Create(
        string name,
        string preparationMethod,
        int levels,
        List<Ingredient> ingredients,
        Category category,
        Rating rating,
        int preparationTime) => new (
            Guid.NewGuid(),
            name,
            preparationMethod,
            levels,
            ingredients,
            category.Id,
            rating,
            preparationTime);
}
