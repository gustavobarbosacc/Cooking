using Cooking.Domain.Users;

namespace Cooking.Application.Users.Find;

public class UserResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Role Role { get; init; }
    public DateTime CreatedOnUtc { get; init; }
}
