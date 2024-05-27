using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Measures;
using Cooking.Domain.Products;
using Cooking.Domain.Recipes;

namespace Cooking.Application.Recipes.Create;

internal class CreateRecipeCommandHandler(
    
    IProductRepository productRepository, 
    IIngredientRepository ingredientRepository,
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateRecipeCommand, Guid>
{
    public readonly IProductRepository _productRepository = productRepository;
    public readonly IIngredientRepository _ingredientRepository = ingredientRepository;
    public readonly IRecipeRepository _recipeRepository = recipeRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {

        var ingredients...
        
           

        var recipe = Recipe.Create(
            request.UserId,
            request.CategoryId,
            request.Name,
            request.PreparationMethod,
            request.Level,
            ingredients,
            request.Rating,
            request.PreparationTime,            
            _dateTimeProvider.UtcNow);

        _recipeRepository.Add(recipe);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return recipe.Id;
    }
}
