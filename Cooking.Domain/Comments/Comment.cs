using Cooking.Domain.Abstraction;
using Cooking.Domain.Recipes;
using Cooking.Domain.Users;

namespace Cooking.Domain.Comments;

public class Comment: Entity
{
    public User User { get; set; }
    public Recipe Recipe { get; set; }
    public string Remark { get; set; }

    public Comment(
        Guid id,
        User user,
        Recipe recipe,
        string remark) : base(id)
    {
        User = user;
        Recipe = recipe;
        Remark = remark;
    }

    public static Comment Create(
        User user,
        Recipe recipe,
        string remark) 
        => new (
            Guid.NewGuid(),
            user,
            recipe,
            remark);
}
