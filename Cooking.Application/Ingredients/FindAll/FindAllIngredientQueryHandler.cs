using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Ingredients.Find;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Ingredients.FindAll;

internal sealed class FindAllIngredientQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindAllIngredientQuery, IReadOnlyList<IngredientResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<IngredientResponse>>> Handle(FindAllIngredientQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        int offset = (request.Page - 1) * request.Size;

        const string sql = """
            SELECT
                "Id",
                "Name",
                "ProductId",
                "Measure",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "ingredients"
            WHERE "RemoveOnUtc" IS NULL
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """;

        var ingredients = await connection.QueryAsync<IngredientResponse>(
            sql,
            new
            {
                Offset = offset,
                PageSize = request.Size
            });

        return ingredients.ToList();
    }
}