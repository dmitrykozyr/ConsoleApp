namespace DDD.Infrastructure.Interfaces.Db.Dapper;

public interface IDapperRepository
{
    Task UpdateProceedFlagOrderInBuffer(Guid guid, bool isSuccess = true, string? errorMessage = null);
}
