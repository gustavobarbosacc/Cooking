using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Categories.Find;

internal sealed class FindCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindCategoryQuery, CategoryResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<CategoryResponse>> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                "Id",
                "Name",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "categories"
            WHERE "Id" = @CategoryId AND "RemoveOnUtc" IS NULL
            """;

        var category = await connection.QueryFirstOrDefaultAsync<CategoryResponse>(
            sql,
            new
            {
                request.CategoryId
            });

        return category;
    }
}
