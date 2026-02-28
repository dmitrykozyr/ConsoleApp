using Infrastructure.Models.RequestModels;

namespace Infrastructure.HttpClient_;

public interface IHttpClientData<T>
{
    Task<T?> GetRequest(string baseAddress, string additionalUrl = "", Dictionary<string, string>? headers = null);

    Task<string?> PostRequestReturnString(string baseAddress, T model);

    Task<PostRequestResponse?> PostRequestReturnStream(string baseAddress, T model);
}
