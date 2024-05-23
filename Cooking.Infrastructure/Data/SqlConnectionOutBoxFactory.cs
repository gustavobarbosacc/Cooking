using Npgsql;
using Cooking.Application.Abstractions.Data;
using System.Data;

namespace Cooking.Infrastructure.Data;

internal sealed class SqlOutBoxConnectionFactory(string connectionString) : ISqlOutBoxConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}