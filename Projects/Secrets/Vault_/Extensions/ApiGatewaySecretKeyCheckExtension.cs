namespace Vault_.Extensions;

public static class ApiGatewaySecretKeyCheckExtension
{
    public static void ApiGatewaySecretKeyCheck(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            const string OCELOT_SECRET_KEY = "ocelotSecretKey";
            const string YARP_SECRET_KEY = "yarpSecretKey";

            bool isOcelotSecretExists = context.Request.Headers.TryGetValue("OcelotGatewaySecretKey", out var ocelotSecret);
            bool isYarpSecretExists = context.Request.Headers.TryGetValue("YarpGatewaySecretKey", out var yarpScret);

            bool isRequestGrantedByOcelot = isOcelotSecretExists && ocelotSecret == OCELOT_SECRET_KEY;
            bool isRequestGrantedByYarp = isYarpSecretExists && yarpScret == YARP_SECRET_KEY;

            if (isRequestGrantedByOcelot || isRequestGrantedByYarp)
            {
                await next();
            }
            else
            {
                context.Response.StatusCode = 403;

                await context.Response.WriteAsync("Прямой доступ запрещен. Используйте API Gateway.");
            }
        });
    }
}
