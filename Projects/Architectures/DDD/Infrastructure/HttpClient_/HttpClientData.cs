using DDD.Domain.Enums;
using DDD.Domain.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace DDD.Infrastructure.HttpClient_;

public class HttpClientData<T>(
   ILoggingService logging,
   IHttpClientFactory httpClientFactory)
  : IHttpClientData<T> where T : class
{
    private readonly ILoggingService _logging = logging;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<T?> GetRequestGeneric(string url, Dictionary<string, string?>? queryParams = null)
    {
        try
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            if (queryParams is not null && queryParams.Count > 0)
            {
                url = QueryHelpers.AddQueryString(url, queryParams);
            }

            httpClient.BaseAddress = new Uri(url);

            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Используем для автоматической десериализации JSON в объект
                T? result = await response.Content.ReadFromJsonAsync<T>();
                return result;
            }
            else
            {
                await _logging.LogToFile(LoggingTypes.Error, $"Ошибка при отправке GET-запроса, статус: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            await _logging.LogToFile(LoggingTypes.Error, $"Исключение при отправке GET-запроса: {ex}");

#if DEBUG
            throw;
#endif
        }

        return null;
    }

    public async Task<string?> PostRequestReturnString(string url, T body, Dictionary<string, string?>? queryParams = null)
    {
        try
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            if (queryParams is not null && queryParams.Count > 0)
            {
                url = QueryHelpers.AddQueryString(url, queryParams);
            }

            httpClient.BaseAddress = new Uri(url);

            ServicePointManager.SecurityProtocol =
             SecurityProtocolType.Tls12 |
             SecurityProtocolType.Tls13;

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, body);
            if (response != null && response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                await _logging.LogToFile(LoggingTypes.Error, $"Ошибка при отправке POST-запроса, статус: {response?.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            await _logging.LogToFile(LoggingTypes.Error, $"Исключение при отправке POST-запроса: {ex}");

#if DEBUG
            throw;
#endif
        }

        return null;
    }

    public async Task<string?> PostFileRequest(string url, string xmlFile)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient();
        using var form = new MultipartFormDataContent();

        var fileContent = new StringContent(xmlFile, Encoding.UTF8, "application/xml");

        form.Add(fileContent, "File", "document.xml");

        using HttpResponseMessage response = await httpClient.PostAsync(url, form);

        string result = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        return result;
    }
}
