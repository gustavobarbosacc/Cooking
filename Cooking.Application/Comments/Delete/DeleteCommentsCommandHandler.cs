using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;
using Cooking.Domain.Comments;

namespace Cooking.Application.Comments.Delete;

internal class DeleteCommentsCommandHandler(
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<DeleteCommentCommand>
{
    public readonly ICommentRepository _commentRepository = commentRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var user =  await _commentRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user == null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }

        user.RemoveOn(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
