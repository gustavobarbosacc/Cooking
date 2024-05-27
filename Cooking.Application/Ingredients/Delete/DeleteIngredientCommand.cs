using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Ingredients.Delete;

public record DeleteIngredientCommand(Guid Id) : ICommand;