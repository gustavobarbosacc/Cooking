using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Users;

namespace Cooking.Application.Users.Delete;

public record DeleteUserCommand(Guid Id) : ICommand;