using Cooking.Domain.Recipes;

namespace Cooking.Api.Controllers.Request;

public class CreateRecipeRequest
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PreparationMethod { get; set; } = string.Empty;
    public int Level { get; set; }
    public List<Guid> Ingredients { get; set; } = [];
    public Rating Rating { get; set; }
    public int PreparationTime { get; set; }
}
