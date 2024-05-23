using Cooking.Application.Abstractions.Clock;

namespace Cooking.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}