using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Users;

public class UserErrors
{
    public static readonly Error NotFound = new(
       "User.NotFound",
       "The User with the specified identifier was not found");
}
