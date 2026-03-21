namespace NTier.DataAccess.Functions.Interfaces;

public interface ICRUD
{
    Task<T> Create<T>(T objectForDb)
        where T : class;

    Task<T> Read<T>(long entityId)
        where T : class;

    Task<List<T>> ReadAll<T>()
        where T : class;

    Task<T> Update<T>(T objectToUpdate, long entityId)
        where T : class;

    Task<bool> Delete<T>(long entityId)
        where T : class;
}
