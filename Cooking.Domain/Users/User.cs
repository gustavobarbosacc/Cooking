using Cooking.Domain.Abstraction;

namespace Cooking.Domain.Users;

public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    public User(
        Guid id,
        string name,
        string email,
        string password,
        Role role) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

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
