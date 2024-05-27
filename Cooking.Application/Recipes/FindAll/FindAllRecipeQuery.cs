using Cooking.Application.Abstractions.Caching;
using Cooking.Application.Recipes.Find;

namespace Cooking.Application.Recipes.FindAll;

public sealed record FindAllRecipeQuery(int Page = 1, int Size = 10) : ICachedQuery<IReadOnlyList<RecipeResponse>>
{
    public string CacheKey => $"recipe-all-{Page}-{Size}";

    public TimeSpan? Expiration => null;
}
