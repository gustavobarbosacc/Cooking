using Cooking.Application.Abstractions.Caching;

namespace Cooking.Application.Categories.Find;

public sealed record FindCategoryQuery(Guid CategoryId) : ICachedQuery<CategoryResponse>
{
    public string CacheKey => $"category-id-{CategoryId}";

    public TimeSpan? Expiration => null;
}
