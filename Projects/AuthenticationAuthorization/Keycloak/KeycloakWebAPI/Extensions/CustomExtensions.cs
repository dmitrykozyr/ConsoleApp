using Microsoft.OpenApi.Models;

namespace KeycloakWebAPI.Extensions;

public static class CustomExtensions
{
    public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(id => id.FullName!.Remove('+', '-'));

            o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openId", "openid" },
                            { "profile", "profile" }
                        }
                    }
                }
            });

            var securityRequirements = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Keycloak",
                            Type = ReferenceType.SecurityScheme
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "Bearer"
                    },
                    []
                }
            };

            o.AddSecurityRequirement(securityRequirements);
        });

        return services;
    }
}
