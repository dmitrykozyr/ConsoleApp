using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Models.Options;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Infrastructure.HttpClient_;

public class HttpClientData<T> : IHttpClientData<T>
    where T : class, new()
{
    private readonly VaultOptions? VaultOptions;

    private readonly ILogging _logging;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientData(IOptions<VaultOptions> vaultOptions, ILogging logging, IHttpClientFactory httpClientFactory)
    {
        VaultOptions = vaultOptions.Value;

        _logging = logging;
        _httpClientFactory = httpClientFactory;
    }

    //! Протестить
    public async Task<T?> GetRequest(string baseAddress, string additionalUrl = "", Dictionary<string, string>? headers = null)
    {
        using (HttpClient client = _httpClientFactory.CreateClient())
        {
            try
            {
                Guard.IsNotNull(VaultOptions);

                client.BaseAddress = new Uri(baseAddress);

                client.DefaultRequestHeaders.Add("X-Vault-Token", VaultOptions.Secret ?? "");

                if (headers is not null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = await client.GetAsync(additionalUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    T? result = JsonSerializer.Deserialize<T>(responseData);

                    return result;
                }
                else
                {
                    _logging.LogToFile($"Ошибка при отправке GET-запроса, статус: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logging.LogToFile($"Исключение при отправке GET-запроса: {ex.Message}");
            }

            return null;
        }
    }

    // Не тестировалось, может потребоваться доработка
    public async Task<bool> PostRequest(string baseAddress, T model)
    {
        using (HttpClient client = _httpClientFactory.CreateClient())
        {
            client.BaseAddress = new Uri(baseAddress);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("endpoint", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    if (result is not null)
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    _logging.LogToFile($"Ошибка при отправке POST-запроса, статус: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logging.LogToFile($"Исключение при отправке POST-запроса: {ex.Message}");
            }

            return false;
        }
    }
}
