/*
    Тестируем проект в Postman

    Генерация токена
    https://localhost:7269/api/login
    Postman -> Body -> raw -> JSON
    {
        "Username": "User1",
        "Password": "pass_1"
    }

    Эндпоинт, не требующий авторизации и аутентификации
    https://localhost:7269/api/user/public

    Эндпоинт, требующий токен юзера с ролью 'Administrator'
    Вставляем его в Postman -> Auth -> Bearer Token
    https://localhost:7269/api/user/admins
*/

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Добавляем для аутентификации
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer              = true,
            ValidateAudience            = true,
            ValidateLifetime            = true,
            ValidateIssuerSigningKey    = true,
            ValidIssuer                 = builder.Configuration["Jwt:Issuer"],
            ValidAudience               = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey            = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Включаем аутентификацию
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

//app.MapRazorPages();
app.Run();
