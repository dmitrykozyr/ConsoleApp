    namespace Dapper_.Interfaces;

public interface IRepository<TResult>
{
    Task<IEnumerable<TResult>> CallProcedure(string procedureName, object? parameters = default);
}
