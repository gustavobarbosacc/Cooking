using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Categories;

public class Category(Guid id, Guid userId, string name) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;

    public static Category Create(Guid userId, string name) 
        => new(Guid.NewGuid(), userId, name);
}

//need to see if we can add more Categories to a Enum
