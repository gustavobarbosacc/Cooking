using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Products.Find;

internal sealed class FindProductQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindProductQuery, ProductResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<ProductResponse>> Handle(FindProductQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                "Id",
                "Name",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "products"
            WHERE "Id" = @ProductId AND "RemoveOnUtc" IS NULL
            """;

        var product = await connection.QueryFirstOrDefaultAsync<ProductResponse>(
            sql,
            new
            {
                request.ProductId
            });

        return product;
    }
}
