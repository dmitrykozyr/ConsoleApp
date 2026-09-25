using DDD.Domain.Enums;
using DDD.Infrastructure.Interfaces.Db.Dapper;
using Domain.Enums;

namespace DDD.Infrastructure.Repositories.Dapper.Custom;

public class DapperRepository(
    IRepository<string> repositoryComplexOrder)
    : IDapperRepository
{
    private readonly IRepository<string> _repositoryComplexOrder = repositoryComplexOrder;

    private readonly DatabaseType databaseType = DatabaseType.General;

    public async Task UpdateProceedFlagOrderInBuffer(Guid guid, bool isSuccess = true, string? errorMessage = null)
    {
        var requestModel = new
        {
            guid,
            isSuccess,
            errorMessage
        };

        await _repositoryComplexOrder.CallProcedure(
            StoreProcedures.SomeProcedure,
            requestModel,
            databaseType);
    }
}
