using Cooking.Domain.Users;

namespace Cooking.Application.Comments.Find;

public class CommentResponse
{
    public Guid Id { get; init; }
    public Guid RecipeId { get; init; }
    public string Remark { get; init; } = string.Empty;
    public DateTime CreatedOnUtc { get; init; }
}