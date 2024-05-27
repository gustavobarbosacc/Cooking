using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Recipes.Find;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Recipes.FindAll;

internal sealed class FindAllRecipeQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindAllRecipeQuery, IReadOnlyList<RecipeResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<RecipeResponse>>> Handle(FindAllRecipeQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        int offset = (request.Page - 1) * request.Size;

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
            WHERE "RemoveOnUtc" IS NULL
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """;

        var recipes = await connection.QueryAsync<RecipeResponse>(
            sql,
            new
            {
                Offset = offset,
                PageSize = request.Size
            });

        return recipes.ToList();
    }
}