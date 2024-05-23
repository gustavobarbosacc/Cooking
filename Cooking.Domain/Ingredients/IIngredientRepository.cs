namespace Cooking.Domain.Ingredients;

public interface IIngredientRepository
{
    Task<Ingredient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Ingredient ingredient);
}
