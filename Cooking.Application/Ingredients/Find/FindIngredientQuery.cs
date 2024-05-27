using Cooking.Application.Abstractions.Caching;

namespace Cooking.Application.Ingredients.Find;

public sealed record FindIngredientQuery(Guid IngredientId) : ICachedQuery<IngredientResponse>
{
    public string CacheKey => $"ingredients-id-{IngredientId}";

    public TimeSpan? Expiration => null;
}
