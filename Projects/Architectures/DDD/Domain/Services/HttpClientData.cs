using Domain.Interfaces;

namespace Domain.Services;

public class HttpClientData<T> : IHttpClientData<T?>
{
    public Task<T?> GetRequest(string additionalRequest)
    {
        throw new NotImplementedException(); //!
    }

    public Task<bool> PostRequest(T model)
    {
        throw new NotImplementedException(); //!
    }
}
