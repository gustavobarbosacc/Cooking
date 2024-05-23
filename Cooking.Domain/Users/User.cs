using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Users;

public class User(
    Guid id,
    string name,
    string email,
    string password,
    Role role) : Entity(id)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public Role Role { get; set; } = role;

    public static User Create(
        string name,
        string email,
        string password,
        Role role) 
            => new (Guid.NewGuid(),
                    name,
                    email,
                    password, 
                    role);
}
