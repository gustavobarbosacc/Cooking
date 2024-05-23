using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Categories;

public class Category(Guid id, string name) : Entity(id)
{
    public string Name { get; set; } = name;

    public static Category Create(string name) 
        => new(Guid.NewGuid(), name);
}

//need to see if we can add more Categories to a Enum
