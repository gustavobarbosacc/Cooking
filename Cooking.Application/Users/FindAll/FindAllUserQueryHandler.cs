using Cooking.Application.Abstractions.Data;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Users.Find;
using Cooking.Domain.Abstractions;
using Dapper;

namespace Cooking.Application.Users.FindAll;

internal sealed class FindAllUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<FindAllUserQuery, IReadOnlyList<UserResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<UserResponse>>> Handle(FindAllUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        int offset = (request.Page - 1) * request.Size;

        const string sql = """
            SELECT
                "Id",
                "Name",
                "Email",
                "Role",
                "CreatedOnUtc" AS "CreatedOnUtc"
            FROM "users"
            WHERE "RemoveOnUtc" IS NULL
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """;

        var products = await connection.QueryAsync<UserResponse>(
            sql,
            new
            {
                Offset = offset,
                PageSize = request.Size
            });

        return products.ToList();
    }
}