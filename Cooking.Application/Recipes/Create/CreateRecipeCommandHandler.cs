using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Recipes;

namespace Cooking.Application.Recipes.Create;

internal class CreateRecipeCommandHandler(   
    ICategoryRepository categoryRepository,
    IIngredientRepository ingredientRepository,
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateRecipeCommand, Guid>
{
    public readonly ICategoryRepository _categoryRepository = categoryRepository;
    public readonly IIngredientRepository _ingredientRepository = ingredientRepository;
    public readonly IRecipeRepository _recipeRepository = recipeRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    { 
        var ingredient = await _ingredientRepository.GetByIdsAsync(request.Ingredients, cancellationToken);
       
        if (ingredient is null)
        {
            return Result.Failure<Guid>(RecipeErrors.NotIngredientFound);
        }

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
       
        if (category is null)
        {
            return Result.Failure<Guid>(RecipeErrors.NotCategoryFound);
        }

        var recipe = Recipe.Create(
            request.UserId,
            request.Title,
            request.PreparationMethod,
            request.Level,
            ingredient,
            category,
            request.Rating,
            request.PreparationTime,            
            _dateTimeProvider.UtcNow);

        _recipeRepository.Add(recipe);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return recipe.Id;
    }
}
