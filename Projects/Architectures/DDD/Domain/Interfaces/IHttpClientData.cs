namespace Domain.Interfaces;

public interface IHttpClientData<T>
{
    Task<T?> GetRequest(string baseAddress, string additionalUrl = "", Dictionary<string, string>? headers = null);

    Task<bool> PostRequest(string baseAddress, T model);
}
