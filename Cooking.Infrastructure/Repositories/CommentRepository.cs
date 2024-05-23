using Cooking.Domain.Comments;

namespace Cooking.Infrastructure.Repositories;

internal class CommentRepository(ApplicationDbContext dbContext) 
    : Repository<Comment>(dbContext), ICommentRepository { }
