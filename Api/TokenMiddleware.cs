using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api
{
    // Создадим совственный компонент Middleware, который подключим в Startup
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        // Класс middleware должен иметь конструктор, который принимает параметр типа RequestDelegate
        // Через него можно получить ссылку на тот делегат запроса, который стоит следующим в конвейере обработки запроса
        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // В классе должен быть метод Invoke или InvokeAsync, должен возвращать Task и принимать HttpContext
        // Он будет обрабатывать запрос
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
}
