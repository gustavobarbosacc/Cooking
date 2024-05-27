using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Products.Find;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Products.FindAll;

internal sealed class FindAllProductQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindAllProductQuery, IReadOnlyList<ProductResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<ProductResponse>>> Handle(FindAllProductQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        int offset = (request.Page - 1) * request.Size;

        const string sql = """
            SELECT
                "Id",
                "Name",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "products"
            WHERE "RemoveOnUtc" IS NULL
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """;

        var products = await connection.QueryAsync<ProductResponse>(
            sql,
            new
            {
                Offset = offset,
                PageSize = request.Size
            });

        return products.ToList();
    }
}