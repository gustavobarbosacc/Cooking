using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Recipes.Delete;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Recipes;

namespace Cooking.Application.Categories.Delete;

internal class DeleteRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<DeleteRecipeCommand>
{
    public readonly IRecipeRepository _recipeRepository = recipeRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<Result> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe =  await _recipeRepository.GetByIdAsync(request.Id, cancellationToken);

        if(recipe == null)
        {
            return Result.Failure(RecipeErrors.NotFound);
        }

        recipe.RemoveOn(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
