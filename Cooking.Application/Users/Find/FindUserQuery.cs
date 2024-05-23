using Cooking.Application.Abstractions.Caching;

namespace Cooking.Application.Users.Find;

public sealed record FindUserQuery(Guid UserId) : ICachedQuery<UserResponse>
{
    public string CacheKey => $"user-id-{UserId}";

    public TimeSpan? Expiration => null;
}
