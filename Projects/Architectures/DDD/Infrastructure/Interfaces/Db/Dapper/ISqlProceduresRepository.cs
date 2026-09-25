using Infrastructure.Models.ResponseModels;

namespace Infrastructure.Interfaces.Db;

public interface ISqlProceduresRepository
{
    Task<DbDataResponseModel> GetDbDataDictionaryLongString(string storeProcedureName);

    Task<List<string>> GetDbDataListString(string storeProcedureName);
}
