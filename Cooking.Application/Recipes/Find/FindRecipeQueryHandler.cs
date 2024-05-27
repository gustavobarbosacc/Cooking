using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Recipes.Find;

internal sealed class FindRecipeQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindRecipeQuery, RecipeResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<RecipeResponse>> Handle(FindRecipeQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                "Id",
                "UserId".
                "CategoryId",
                "Name",
                "PreparationMethod",
                "Levels",
                "Ingredients",
                "Rating",
                "PreparationTime",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "recipes"
            WHERE "Id" = @CategoryId AND "RemoveOnUtc" IS NULL
            """;

        var recipe = await connection.QueryFirstOrDefaultAsync<RecipeResponse>(
            sql,
            new
            {
                request.RecipeId
            });

        return recipe;
    }
}
