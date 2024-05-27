using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Products.Create;

public record CreateProductCommand(
    Guid UserId,
    string Name) : ICommand<Guid>;