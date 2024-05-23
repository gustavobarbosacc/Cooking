namespace Cooking.Domain.Recipes;

public interface IRecipeRepository
{
    Task<Recipe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Recipe recipe);
}
