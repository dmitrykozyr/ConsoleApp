using DDD.Domain.Enums;

namespace DDD.Infrastructure.Interfaces.Db.Dapper;

public interface IRepository<TResult>
{
    Task<IEnumerable<TResult>?> CallProcedure(string procedureName, object? parameters, DatabaseType databaseType);

    string GetConnectionStringForCurrentDb(DatabaseType databaseType);
}
