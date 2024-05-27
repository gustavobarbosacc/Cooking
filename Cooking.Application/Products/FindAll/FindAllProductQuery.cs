using Cooking.Application.Abstractions.Caching;
using Cooking.Application.Products.Find;

namespace Cooking.Application.Products.FindAll;

public sealed record FindAllProductQuery(int Page = 1, int Size = 10) : ICachedQuery<IReadOnlyList<ProductResponse>>
{
    public string CacheKey => $"products-all-{Page}-{Size}";

    public TimeSpan? Expiration => null;
}
