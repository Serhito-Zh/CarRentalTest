using System.Data;
using Dapper;

namespace CarRental.Infrastructure.Persistence.Handlers;

/// <summary>
/// Handler for conversion SQLite string guid to dapper guid
/// </summary>
public sealed class SQLiteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid) 
        => parameter.Value = guid.ToString();

    public override Guid Parse(object value) => new Guid((string)value);
}