using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Ingredients;

public class IngredientErrors
{
    public static readonly Error NotFound = new(
       "Ingredient.NotFound",
       "The Ingredient with the specified identifier was not found");

    public static readonly Error NotProductFound = new(
        "Product.NotProductFound",
        "The Product with the specified identifier was not found");
}
