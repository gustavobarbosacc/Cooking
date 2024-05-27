using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Ingredients;

namespace Cooking.Application.Ingredients.Create;

public record CreateIngredientCommand(
    Guid UserId,
    Guid ProductId,
    string Name,
    Measure measure) : ICommand<Guid>;
    