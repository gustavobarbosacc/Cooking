using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Comments;

public class CommentErrors
{
    public static readonly Error NotFound = new(
       "Comments.NotFound",
       "The comments with the specified identifier was not found");

    public static readonly Error NotRecipeFound = new(
        "Recipe.NotRecipeFound",
        "The Recipe with the specified identifier was not found");
}
