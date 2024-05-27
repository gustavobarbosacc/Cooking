using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Recipes;

public class RecipeErrors
{
    public static readonly Error NotFound = new(
       "Recipe.NotFound",
       "The Recipe with the specified identifier was not found");
}
