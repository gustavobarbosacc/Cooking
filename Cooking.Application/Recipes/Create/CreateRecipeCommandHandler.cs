using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Recipes;

namespace Cooking.Application.Recipes.Create;

internal class CreateRecipeCommandHandler(   
    IIngredientRepository ingredientRepository,
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateRecipeCommand, Guid>
{
    public readonly IIngredientRepository _ingredientRepository = ingredientRepository;
    public readonly IRecipeRepository _recipeRepository = recipeRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {


        var ingredientList = new List<Ingredient>();

       

        var recipe = Recipe.Create(
            request.UserId,
            request.CategoryId,
            request.Name,
            request.PreparationMethod,
            request.Level,
          //  ingredientList,
            request.Rating,
            request.PreparationTime,            
            _dateTimeProvider.UtcNow);

        _recipeRepository.Add(recipe);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return recipe.Id;
    }
}
