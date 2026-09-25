namespace DDD.Domain.Interfaces;

public interface IHttpClientData<T>
{
    Task<T?> GetRequestGeneric(string url, Dictionary<string, string?>? queryParams = null);

    Task<string?> PostRequestReturnString(string url, T body, Dictionary<string, string?>? queryParams = null);

    Task<string?> PostFileRequest(string url, string xmlFile);
}
