using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Comments.Find;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Comments.FindAll;

internal sealed class FindAllCommentQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindAllCommentQuery, IReadOnlyList<CommentResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<CommentResponse>>> Handle(FindAllCommentQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        int offset = (request.Page - 1) * request.Size;

        const string sql = """
            SELECT
                "Id",
                "RecipeId",
                "Remark",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "comments"
            WHERE "RemoveOnUtc" IS NULL
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """;

        var categories = await connection.QueryAsync<CommentResponse>(
            sql,
            new
            {
                Offset = offset,
                PageSize = request.Size
            });

        return categories.ToList();
    }
}