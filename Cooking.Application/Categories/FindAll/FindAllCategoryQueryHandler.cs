using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Categories.Find;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Categories.FindAll;

internal sealed class FindAllCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindAllCategoryQuery, IReadOnlyList<CategoryResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<CategoryResponse>>> Handle(FindAllCategoryQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        int offset = (request.Page - 1) * request.Size;

        const string sql = """
            SELECT
                "Id",
                "Name",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "categories"
            WHERE "RemoveOnUtc" IS NULL
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """;

        var categories = await connection.QueryAsync<CategoryResponse>(
            sql,
            new
            {
                Offset = offset,
                PageSize = request.Size
            });

        return categories.ToList();
    }
}