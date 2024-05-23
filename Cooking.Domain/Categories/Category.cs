using Cooking.Domain.Abstraction;

namespace Cooking.Domain.Categories;

public class Category : Entity
{
    public string Name { get; set; }

    public Category(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public static Category Create(string name) 
        => new(Guid.NewGuid(), name);
}

//need to see if we can add more Categories to a Enum
