using Microsoft.EntityFrameworkCore;
using Cooking.Domain.Orders;

namespace Cooking.Infrastructure.Repositories;

internal sealed class OrderRepository(ApplicationDbContext dbContext)
    : Repository<Order>(dbContext), IOrderRepository
{
    public async Task<Order?> GetIdByUserAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
           .Set<Order>()
           .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
    }
}
