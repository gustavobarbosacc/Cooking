using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Ingredients.Find;

internal sealed class FindIngredientQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindIngredientQuery, IngredientResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IngredientResponse>> Handle(FindIngredientQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                "Id",
                "CategoryId",
                "Name",
                "Measure",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "ingredients"
            WHERE "Id" = @IngredientId AND "RemoveOnUtc" IS NULL
            """;

        var ingredient = await connection.QueryFirstOrDefaultAsync<IngredientResponse>(
            sql,
            new
            {
                request.IngredientId
            });

        return ingredient;
    }
}
