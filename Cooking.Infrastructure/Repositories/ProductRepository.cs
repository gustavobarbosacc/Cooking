using Cooking.Domain.Products;

namespace Cooking.Infrastructure.Repositories;

internal class ProductRepository(ApplicationDbContext dbContext)
    : Repository<Product>(dbContext), IProductRepository { }
