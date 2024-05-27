using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Comments.Delete;

public record DeleteCommentCommand(Guid Id) : ICommand;