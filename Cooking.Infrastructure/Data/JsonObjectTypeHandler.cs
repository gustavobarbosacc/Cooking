using Dapper;
using Newtonsoft.Json;
using System.Data;


namespace Cooking.Infrastructure.Data;

public class JsonObjectTypeHandler : SqlMapper.ITypeHandler
{
    public void SetValue(IDbDataParameter parameter, object value)
    {
        parameter.Value = (value == null)
            ? (object)DBNull.Value
            : JsonConvert.SerializeObject(value);
        parameter.DbType = DbType.String;
    }

    public object Parse(Type destinationType, object value) 
        => JsonConvert.DeserializeObject(value.ToString()!, destinationType)!;
}