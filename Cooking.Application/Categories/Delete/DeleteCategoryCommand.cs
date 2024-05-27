using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Categories.Delete;

public record DeleteCategoryCommand(Guid Id) : ICommand;