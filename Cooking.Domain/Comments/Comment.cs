using Cooking.Domain.Abstractions;
using Cooking.Domain.Recipes;
using Cooking.Domain.Users;

namespace Cooking.Domain.Comments;

public class Comment(
    Guid id,
    Guid userId,
    Guid recipeId,
    string remark) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public Guid RecipeId { get; set; } = recipeId;
    public string Remark { get; set; } = remark;

    public static Comment Create(
        Guid userId,
        Recipe recipe,
        string remark) 
        => new (
            Guid.NewGuid(),
            userId,
            recipe.Id,
            remark);
}
