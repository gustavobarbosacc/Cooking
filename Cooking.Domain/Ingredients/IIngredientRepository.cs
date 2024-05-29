using Cooking.Domain.Products;

namespace Cooking.Domain.Ingredients;

public interface IIngredientRepository
{
    Task<Ingredient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Ingredient ingredient);
    Task<List<Ingredient>?> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken? cancellationToken = default);
}
