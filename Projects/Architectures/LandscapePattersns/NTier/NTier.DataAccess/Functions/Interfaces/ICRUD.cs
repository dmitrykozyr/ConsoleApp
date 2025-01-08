namespace NTier.DataAccess.Functions.Interfaces;

public interface ICRUD
{
    Task<T> Create<T>(T objectForDb) where T : class;
    Task<T> Read<T>(Int64 entityId) where T : class;
    Task<List<T>> ReadAll<T>() where T : class;
    Task<T> Update<T>(T objectToUpdate, Int64 entityId) where T : class;
    Task<bool> Delete<T>(Int64 entityId) where T : class;
}
