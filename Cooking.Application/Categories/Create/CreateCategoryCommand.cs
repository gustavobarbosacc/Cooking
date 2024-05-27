using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Categories.Create;

public record CreateCategoryCommand(
    Guid UserId,
    string Name) : ICommand<Guid>;