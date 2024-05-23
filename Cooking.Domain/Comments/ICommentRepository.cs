using Cooking.Domain.Categories;

namespace Cooking.Domain.Comments;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Comment comment);
}
