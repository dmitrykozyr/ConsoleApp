using Domain.Models.ResponseModels;

namespace Domain.Interfaces.Repositories;

public interface ISqlProceduresRepository
{
    DbDataResponseModel GetDbDataDictionaryLongString(string storeProcedureName);

    List<string> GetDbDataListString(string storeProcedureName);
}
