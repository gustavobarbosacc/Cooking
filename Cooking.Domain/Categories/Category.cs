using Cooking.Domain.Abstractions;
using Cooking.Domain.Users;

namespace Cooking.Domain.Categories;

public class Category(
    Guid id, 
    Guid userId, 
    string name, 
    DateTime CreatedOnUtc) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;

    public DateTime CreatedOnUtc { get; internal set; } = CreatedOnUtc;
    public DateTime? UpdatedOnUtc { get; internal set; }
    public DateTime? RemoveOnUtc { get; internal set; }

    public static Category Create(Guid userId, string name, DateTime utcNow) 
        => new(Guid.NewGuid(), userId, name, utcNow);

    public Result RemoveOn(DateTime utcNow)
    {
        if (RemoveOnUtc is not null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }

        RemoveOnUtc = utcNow;

        return Result.Success();
    }
}


