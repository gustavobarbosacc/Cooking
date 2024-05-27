using Cooking.Application.Abstractions.Messaging;

namespace Cooking.Application.Comments.Create;

public record CreateCommentCommand(
    Guid UserId,
    Guid RecipeId,
    string Remark) : ICommand<Guid>;