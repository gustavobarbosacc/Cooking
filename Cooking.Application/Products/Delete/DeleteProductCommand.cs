using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Products.Delete;

public record DeleteProductCommand(Guid Id) : ICommand;