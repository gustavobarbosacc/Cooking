using Cooking.Application.Abstractions.Caching;
using Cooking.Application.Categories.Find;
using Cooking.Application.Users.Find;

namespace Cooking.Application.Categories.FindAll;

public sealed record FindAllCategoryQuery(int Page = 1, int Size = 10) : ICachedQuery<IReadOnlyList<CategoryResponse>>
{
    public string CacheKey => $"category-all-{Page}-{Size}";

    public TimeSpan? Expiration => null;
}
