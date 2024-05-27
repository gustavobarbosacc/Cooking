using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Comments.Find;

internal sealed class FindCommentQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindCommentQuery, CommentResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<CommentResponse>> Handle(FindCommentQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                "Id",
                "RecipeId",
                "Remark",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "comments"
            WHERE "Id" = @CommentId AND "RemoveOnUtc" IS NULL
            """;

        var comment = await connection.QueryFirstOrDefaultAsync<CommentResponse>(
            sql,
            new
            {
                request.CommentId
            });

        return comment;
    }
}
