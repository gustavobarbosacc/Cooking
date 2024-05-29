using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Recipes;

namespace Cooking.Application.Recipes.Create;

public record CreateRecipeCommand(
    Guid UserId,
    Guid CategoryId,
    string Title,
    string PreparationMethod,
    int Level,
    List<Guid> Ingredients,
    Rating Rating,
    int PreparationTime) : ICommand<Guid>;