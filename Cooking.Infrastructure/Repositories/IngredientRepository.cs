using Cooking.Domain.Ingredients;

namespace Cooking.Infrastructure.Repositories;

internal class IngredientRepository(ApplicationDbContext dbContext)
    : Repository<Ingredient>(dbContext), IIngredientRepository { }
