using Infrastructure.Models.ResponseModels;

namespace Infrastructure.Interfaces.Db;

public interface ISqlProceduresRepository
{
    DbDataResponseModel GetDbDataDictionaryLongString(string storeProcedureName);

    List<string> GetDbDataListString(string storeProcedureName);
}
