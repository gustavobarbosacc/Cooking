using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Users;

namespace Cooking.Application.Users.Create;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    Role Role) : ICommand<Guid>;