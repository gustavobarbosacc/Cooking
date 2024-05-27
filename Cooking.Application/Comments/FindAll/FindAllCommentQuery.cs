using Cooking.Application.Abstractions.Caching;
using Cooking.Application.Comments.Find;

namespace Cooking.Application.Comments.FindAll;

public sealed record FindAllCommentQuery(int Page = 1, int Size = 10) : ICachedQuery<IReadOnlyList<CommentResponse>>
{
    public string CacheKey => $"category-all-{Page}-{Size}";

    public TimeSpan? Expiration => null;
}
