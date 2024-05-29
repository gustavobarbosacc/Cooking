using Cooking.Domain.Ingredients;
using Cooking.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Cooking.Infrastructure.Repositories;

internal class IngredientRepository(ApplicationDbContext dbContext)
    : Repository<Ingredient>(dbContext), IIngredientRepository
{
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken? cancellationToken = null)
    {
        return await GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Ingredient>?> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await DbContext
           .Set<Ingredient>()
           .Where(x => ids.Contains(x.Id)).ToListAsync(); 
    }    
}
