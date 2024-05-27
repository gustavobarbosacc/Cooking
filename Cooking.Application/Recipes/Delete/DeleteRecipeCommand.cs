using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Recipes.Delete;

public record DeleteRecipeCommand(Guid Id) : ICommand;