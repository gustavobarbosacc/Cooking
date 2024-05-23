using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Users;

public class User(
    Guid id,
    string name,
    string email,
    string password,
    Role role,
    DateTime CreatedOnUtc) : Entity(id)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public Role Role { get; set; } = role;

    public DateTime CreatedOnUtc { get; internal set; } = CreatedOnUtc;
    public DateTime? UpdatedOnUtc { get; internal set; }
    public DateTime? RemoveOnUtc { get; internal set; }

    public static User Create(
        string name,
        string email,
        string password,
        Role role,
        DateTime utcNow) 
            => new (Guid.NewGuid(),
                    name,
                    email,
                    password, 
                    role,
                    utcNow);

    public Result RemoveOn(DateTime utcNow)
    {
        if (RemoveOnUtc is not null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        RemoveOnUtc = utcNow;

        return Result.Success();
    }
}
