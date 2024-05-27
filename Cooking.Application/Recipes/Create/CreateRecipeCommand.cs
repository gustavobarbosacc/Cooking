using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Measures;
using Cooking.Domain.Recipes;

namespace Cooking.Application.Recipes.Create;

public record CreateRecipeCommand(
    Guid UserId,
    Guid CategoryId,
    string Name,
    string PreparationMethod,
    int Level,
    List<Ingredient> Ingredients,
    Measure measure,
    Rating Rating,
    int PreparationTime) : ICommand<Guid>;