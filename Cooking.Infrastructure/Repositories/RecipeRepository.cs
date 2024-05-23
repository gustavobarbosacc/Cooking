using Cooking.Domain.Recipes;

namespace Cooking.Infrastructure.Repositories;

internal class RecipeRepository(ApplicationDbContext dbContext) 
    : Repository<Recipe>(dbContext), IRecipeRepository { }
