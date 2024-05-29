using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Products;
using Cooking.Domain.Users;
using System.Xml.Linq;

namespace Cooking.Application.Ingredients.Create;

internal class CreateIngredientCommandHandler(
    IProductRepository productRepository,
    IIngredientRepository ingredientRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateIngredientCommand, Guid>
{
    public readonly IProductRepository _productRepository = productRepository;
    public readonly IIngredientRepository _ingredientRepository = ingredientRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure<Guid>(IngredientErrors.NotProductFound);
        }       

        var ingredient = Ingredient.Create(
            request.UserId,
            product.Id,
            request.Measure,
            request.Name,
            request.Quantity,
            _dateTimeProvider.UtcNow);

    _ingredientRepository.Add(ingredient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ingredient.Id;
    }
}
