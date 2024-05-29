using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Recipes;

public class RecipeErrors
{
    public static readonly Error NotFound = new(
       "Recipe.NotFound",
       "The Recipe with the specified identifier was not found");
   
    public static readonly Error NotCategoryFound = new(
       "Category.NotFound",
       "The Category with the specified identifier was not found");

    public static readonly Error NotIngredientFound = new(
       "Ingredient.NotFound",
       "The Ingredient with the specified identifier was not found");
}
