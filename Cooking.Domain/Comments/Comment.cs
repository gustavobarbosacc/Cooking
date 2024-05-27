using Cooking.Domain.Abstractions;
using Cooking.Domain.Recipes;

namespace Cooking.Domain.Comments;

public class Comment(
    Guid id,
    Guid userId,
    Guid recipeId,
    string remark,
    DateTime CreatedOnUtc) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public Guid RecipeId { get; set; } = recipeId;
    public string Remark { get; set; } = remark;

    public DateTime CreatedOnUtc { get; internal set; } = CreatedOnUtc;
    public DateTime? UpdatedOnUtc { get; internal set; }
    public DateTime? RemoveOnUtc { get; internal set; }

    public static Comment Create(
        Guid userId,
        Recipe recipe,
        string remark,
        DateTime utcNow) 
        => new (
            Guid.NewGuid(),
            userId,
            recipe.Id,
            remark,
            utcNow);

    public Result RemoveOn(DateTime utcNow)
    {
        if (RemoveOnUtc is not null)
        {
            return Result.Failure(CommentErrors.NotFound);
        }

        RemoveOnUtc = utcNow;

        return Result.Success();
    }
}
