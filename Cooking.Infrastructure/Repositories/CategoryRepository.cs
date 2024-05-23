using Cooking.Domain.Categories;

namespace Cooking.Infrastructure.Repositories;

internal class CategoryRepository(ApplicationDbContext dbContext) 
    : Repository<Category>(dbContext), ICategoryRepository { }
