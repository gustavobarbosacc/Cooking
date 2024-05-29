using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Comments;
using Cooking.Domain.Recipes;

namespace Cooking.Application.Comments.Create;

internal class CreateCommentCommandHandler(
    ICommentRepository commentRepository,
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateCommentCommand, Guid>
{
    public readonly ICommentRepository _commentRepository = commentRepository;
    public readonly IRecipeRepository _recipeRepository = recipeRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _recipeRepository.GetByIdAsync(request.RecipeId, cancellationToken);

        if(recipe is null)
        {
            return Result.Failure<Guid>(CommentErrors.NotRecipeFound);
        }

        var comment = Comment.Create(
            request.UserId,
            recipe,
            request.Remark,
            _dateTimeProvider.UtcNow);

        _commentRepository.Add(comment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
