namespace Cooking.Api.Controllers.Request;

public class CreateCommentRequest
{
    public Guid RecipeId { get; set; }
    public string Remark { get; set; } = string.Empty;
}