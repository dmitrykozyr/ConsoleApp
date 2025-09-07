using KeycloakWebApp.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.RequireHttpsMetadata = false; // Только для ДЕВ
    options.MetadataAddress = builder.Configuration["Authentication:MetadataAddress"];
    options.Authority       = builder.Configuration["Keycloak:Authority"];
    options.ClientId        = builder.Configuration["Keycloak:ClientId"];
    options.ClientSecret    = builder.Configuration["Keycloak:ClientSecret"];
    options.ResponseType    = "code";   // или "id_token" в зависимости от конфигурации
    options.SaveTokens      = true;     // Сохраняет токены в куках
    options.Scope.Add("openid");
    options.Scope.Add("profile");

    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProvider = context =>
        {
            // Здесь можно добавить дополнительные параметры, если необходимо
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            // Здесь можно добавить дополнительную логику после успешной аутентификации
            return Task.CompletedTask;
        }
    };
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
