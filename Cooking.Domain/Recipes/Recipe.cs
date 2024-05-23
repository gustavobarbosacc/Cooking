using Cooking.Domain.Abstraction;
using Cooking.Domain.Categories;
using Cooking.Domain.Ingredients;

namespace Cooking.Domain.Recipes;

public class Recipe : Entity
{
    public string Name { get; set; }
    public string PreparationMethod { get; set; }
    public int Levels { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public Category Category { get; set; }
    public Rating Rating { get; set; }
    public int PreparationTime { get; set; }

    public Recipe(
        Guid id,
        string name,
        string preparationMethod,
        int levels,
        List<Ingredient> ingredients,
        Category category,
        Rating rating,
        int preparationTime) : base(id)
    {
        Name = name;
        PreparationMethod = preparationMethod;
        Levels = levels;
        Ingredients = ingredients;
        Category = category;
        Rating = rating;
        PreparationTime = preparationTime;
    }

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
            category,
            rating,
            preparationTime);
}
