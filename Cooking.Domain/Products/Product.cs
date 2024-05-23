using Cooking.Domain.Abstractions;
using Cooking.Domain.Users;

namespace Cooking.Domain.Products;

public class Product(Guid id, Guid userId, string name) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;

    public static Product Create(Guid userId, string name)
            => new(Guid.NewGuid(), userId, name);
}
