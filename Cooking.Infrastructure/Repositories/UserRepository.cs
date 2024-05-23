using Cooking.Domain.Users;

namespace Cooking.Infrastructure.Repositories;

internal class UserRepository(ApplicationDbContext dbContext) 
    : Repository<User>(dbContext), IUserRepository { }
