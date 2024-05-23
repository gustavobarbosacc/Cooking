using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Users;

public class UserErrors
{
    public static readonly Error NotFound = new(
       "User.NotFound",
       "The User with the specified identifier was not found");

    public static readonly Error Canceled = new(
        "User.Canceled",
        "The Wallet without funds");

    public static readonly Error Accept = new(
        "User.Accept",
        "Unable to change the Accepted status of the order");

    public static readonly Error Finalized = new(
        "Order.Finalized",
        "The Order has already been finished");
}
