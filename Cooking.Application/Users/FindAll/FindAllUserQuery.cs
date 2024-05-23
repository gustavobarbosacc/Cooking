using Cooking.Application.Abstractions.Caching;
using Cooking.Application.Users.Find;

namespace Cooking.Application.Users.FindAll;

public sealed record FindAllUserQuery(int Page = 1, int Size = 10) : ICachedQuery<IReadOnlyList<UserResponse>>
{
    public string CacheKey => $"user-all-{Page}-{Size}";

    public TimeSpan? Expiration => null;
}
