namespace WebApi;

// Кастомный Middleware
public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    // Конструктор должен принимать тип RequestDelegate
    // Через него можно получить ссылку на тот делегат запроса, который стоит следующим в конвейере обработки запроса
    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Метод Invoke или InvokeAsync обрабатывает запрос, должен принимать HttpContext и возвращать Task
    public async Task InvokeAsync(HttpContext context)
    {
        // Получаем из запроса параметр "token"
        // Если он равен "12345678", то передаем запрос следующему компоненту, иначе возвращаем сообщение об ошибке
        var token = context.Request.Query["token"];
        if (token != "12345678")
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Token is invalid");
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}
