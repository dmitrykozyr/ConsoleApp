using Infrastructure.Models.ResponseModels;

namespace Infrastructure.Repositories;

public interface ISqlProceduresRepository
{
    DbDataResponseModel GetDbDataDictionaryLongString(string storeProcedureName);

    List<string> GetDbDataListString(string storeProcedureName);
}
