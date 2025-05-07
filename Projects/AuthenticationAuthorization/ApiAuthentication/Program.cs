using ApiAuthentication.Models;
using ApiAuthentication.Services;
using ApiAuthentication.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// Запуск проекта
// https://localhost:7202/swagger/index.html

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    // Настраиваем, чтобы в сваггере появилась кнопка 'Authorization',
    // чтобы можно было добавить JWT Token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
app.UseAuthorization();
app.UseAuthentication();

app.UseSwagger();

app.MapGet("/", 
    () => "Hello World!");

app.MapPost("/login", 
    (UserLogin user, IUserService service) => Login(user, service));

app.MapPost("/create", 
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    (Movie movie, IMovieService service) => Create(movie, service));

app.MapGet("/get",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
    (int id, IMovieService service) => Get(id, service));

app.MapGet("/list", 
    (IMovieService service) => GetAll(service));

app.MapPut("/update", 
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    (Movie newMovie, IMovieService service) => Update(newMovie, service));

app.MapDelete("/delete",
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    (int id, IMovieService service) => Delete(id, service));

IResult Login(UserLogin user, IUserService service)
{
    if (!string.IsNullOrEmpty(user.UserName) &&
        !string.IsNullOrEmpty(user.Password))
    {
        var loggedInUser = service.Get(user);

        if (loggedInUser == null)
        {
            return Results.NotFound("User not found");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.UserName),
            new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
            new Claim(ClaimTypes.GivenName, loggedInUser.GivenName),
            new Claim(ClaimTypes.Surname, loggedInUser.Surname),
            new Claim(ClaimTypes.Role, loggedInUser.Role)
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(tokenString);
    }

    return Results.NotFound("User with this login|password not found");
}

IResult Create(Movie movie, IMovieService service)
{
    var result = service.Create(movie);

    return Results.Ok(result);
}

IResult Get(int id, IMovieService service)
{
    var result = service.Get(id);

    if (result == null)
    {
        return Results.NotFound("Movie not found");
    }

    return Results.Ok(result);
}

IResult GetAll(IMovieService service)
{
    var result = service.GetAll();

    return Results.Ok(result);
}

IResult Update(Movie newMovie, IMovieService service)
{
    var result = service.Update(newMovie);

    if (result == null)
    {
        return Results.NotFound("Movie not found");
    }

    return Results.Ok(result);
}

IResult Delete(int id, IMovieService service)
{
    var result = service.Delete(id);

    if (result == false)
    {
        return Results.BadRequest("Something went wrong");
    }

    return Results.Ok(result);
}

app.UseSwaggerUI();

app.Run();
