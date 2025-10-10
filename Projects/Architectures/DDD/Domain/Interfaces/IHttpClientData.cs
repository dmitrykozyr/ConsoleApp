using Domain.Models.RequentModels;

namespace Domain.Interfaces;

public interface IHttpClientData<T>
{
    Task<T?> GetRequest(string baseAddress, string additionalUrl = "", Dictionary<string, string>? headers = null);

    Task<string?> PostRequestReturnString(string baseAddress, T model);

    Task<PostRequestResponse?> PostRequestReturnStream(string baseAddress, T model);
}
