namespace Domain.Interfaces;

public interface IHttpClientData<T>
{
    Task<T?> GetRequest(string additionalRequest);

    Task<bool> PostRequest(T model);
}
