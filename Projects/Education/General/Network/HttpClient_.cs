namespace Education.General.Network;

public class HttpClient_
{
    public static async Task<string> GetHttpData_1(IHttpClientFactory httpClientFactory, string url)
    {
        HttpClient httpClient = httpClientFactory.CreateClient();

        using var message = new HttpRequestMessage(HttpMethod.Get, url);

        using HttpResponseMessage responseMessage = await httpClient.SendAsync(message);

        // Читаем ответ после проверки успешности запроса
        responseMessage.EnsureSuccessStatusCode();

        string result = await responseMessage.Content.ReadAsStringAsync();

        return result;
    }

    public static async Task<string> GetHttpData_2(IHttpClientFactory httpClientFactory, string url)
    {
        HttpClient httpClient = httpClientFactory.CreateClient();

        var result = await httpClient.GetStringAsync(url);

        return result;
    }

    // Получение данных с веб-сайта через HttpClient
    public static async Task Main_()
    {
        //var builder = Host.CreateApplicationBuilder();
        //builder.Services.AddHttpClient();
        //using IHost host = builder.Build();

        //var factory = host.Services.GetRequiredService<IHttpClientFactory>();

        //string result_1 = await GetHttpData_1(factory, "https://google.ru/");
        //string result_2 = await GetHttpData_2(factory, "https://google.ru/");
    }
}
