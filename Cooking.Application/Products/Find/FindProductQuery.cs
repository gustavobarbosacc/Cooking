using Cooking.Application.Abstractions.Caching;

namespace Cooking.Application.Products.Find;

public sealed record FindProductQuery(Guid ProductId) : ICachedQuery<ProductResponse>
{
    public string CacheKey => $"products-id-{ProductId}";

    public TimeSpan? Expiration => null;
}
