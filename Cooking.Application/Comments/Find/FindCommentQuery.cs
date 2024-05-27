using Cooking.Application.Abstractions.Caching;

namespace Cooking.Application.Comments.Find;

public sealed record FindCommentQuery(Guid CommentId) : ICachedQuery<CommentResponse>
{
    public string CacheKey => $"comment-id-{CommentId}";

    public TimeSpan? Expiration => null;
}
