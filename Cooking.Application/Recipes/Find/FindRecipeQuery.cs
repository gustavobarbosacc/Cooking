using Cooking.Application.Abstractions.Caching;

namespace Cooking.Application.Recipes.Find;

public sealed record FindRecipeQuery(Guid RecipeId) : ICachedQuery<RecipeResponse>
{
    public string CacheKey => $"recipe-id-{RecipeId}";

    public TimeSpan? Expiration => null;
}
