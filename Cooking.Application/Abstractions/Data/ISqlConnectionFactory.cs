using System.Data;

namespace Cooking.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}

public interface ISqlOutBoxConnectionFactory
{
    IDbConnection CreateConnection();
}