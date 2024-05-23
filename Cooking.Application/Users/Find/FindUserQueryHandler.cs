using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Users.Find;

internal sealed class FindUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindUserQuery, UserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<UserResponse>> Handle(FindUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                "Id",
                "Name",
                "Email",
                "Role",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "users"
            WHERE "Id" = @UserId AND "RemoveOnUtc" IS NULL
            """;

        var user = await connection.QueryFirstOrDefaultAsync<UserResponse>(
            sql,
            new
            {
                request.UserId
            });

        return user;
    }
}
