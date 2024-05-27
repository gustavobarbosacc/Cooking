using Cooking.Application.Abstractions.Caching;
using Cooking.Application.Ingredients.Find;

namespace Cooking.Application.Ingredients.FindAll;

public sealed record FindAllIngredientQuery(int Page = 1, int Size = 10) : ICachedQuery<IReadOnlyList<IngredientResponse>>
{
    public string CacheKey => $"ingredients-all-{Page}-{Size}";

    public TimeSpan? Expiration => null;
}
