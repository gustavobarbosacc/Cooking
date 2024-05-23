using Cooking.Application.Abstractions.Caching;
using Cooking.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cooking.Application.Abstractions.Behaviors;

internal sealed class QueryCachingBehavior<TRequest, TResponse>(
    ILogger<QueryCachingBehavior<TRequest, TResponse>> logger,
    ICacheService cacheService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result
{
    private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger = logger;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var cacheResult = await _cacheService
            .GetAsync<TResponse>(
                request.CacheKey,
                cancellationToken);

        string name = typeof(TRequest).Name;

        if (cacheResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", name);
            return cacheResult;
        }

        _logger.LogInformation("Cache miss for {Query}", name);

        var result = await next();

        if (result.IsSuccess)
        {
            await _cacheService.SetAsync(
                request.CacheKey,
                result,
                request.Expiration,
                cancellationToken);
        }

        return result;
    }
}
