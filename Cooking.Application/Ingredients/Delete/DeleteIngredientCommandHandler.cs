using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Products;

namespace Cooking.Application.Ingredients.Delete;

internal class DeleteIngredientCommandHandler(
    IIngredientRepository ingredientRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<DeleteIngredientCommand>
{
    public readonly IIngredientRepository _ingredientRepositoryy = ingredientRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<Result> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient =  await _ingredientRepositoryy.GetByIdAsync(request.Id, cancellationToken);

        if(ingredient == null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        ingredient.RemoveOn(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
