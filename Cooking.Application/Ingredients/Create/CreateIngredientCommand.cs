using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Ingredients;

namespace Cooking.Application.Ingredients.Create;

public record CreateIngredientCommand(
    Guid UserId,
    Guid ProductId,
    Measure Measure,
    string Name,
    double Quantity) : ICommand<Guid>;
