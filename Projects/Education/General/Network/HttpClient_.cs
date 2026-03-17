namespace Education.General.Network;

class HttpClient_
{
    // Получение данных с веб-сайта через HttpClient
    static async Task Main_(IHttpClientFactory httpClientFactory)
    {
        HttpClient httpClient = httpClientFactory.CreateClient();

        var message = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://google.ru/")
        };

        HttpResponseMessage result = httpClient.Send(message);

        var data = await result.Content.ReadAsStringAsync();
    }
}
