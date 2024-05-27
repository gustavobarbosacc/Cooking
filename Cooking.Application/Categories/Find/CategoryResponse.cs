using Cooking.Domain.Users;

namespace Cooking.Application.Categories.Find;

public class CategoryResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime CreatedOnUtc { get; init; }
}
