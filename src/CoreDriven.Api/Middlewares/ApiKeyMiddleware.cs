using CoreDriven.Data.Configuration;
using CoreDriven.Data.Providers;

namespace CoreDriven.Api.Middlewares;

public class ApiKeyMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ITenantProvider tenantProvider, IConnectionResolver connectionResolver)
    {
        var path = context.Request.Path;
        // Rutas públicas
        if (path.StartsWithSegments("/scalar", StringComparison.OrdinalIgnoreCase) ||
            path.Equals("/openapi/v1.json", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }
        // Obtener el tenant actual
        var currentTenant = tenantProvider.GetTenant();
        if (string.IsNullOrEmpty(currentTenant))
        {
            throw new UnauthorizedAccessException("Tenant not specified");
        }
        // Obtener el API Key esperado para este tenant
        string expectedApiKey;
        try
        {
            expectedApiKey = connectionResolver.GetApiKey(currentTenant);
        }
        catch (KeyNotFoundException)
        {
            throw new UnauthorizedAccessException("Invalid tenant");
        }
        // Validar API Key
        if (!context.Request.Headers.TryGetValue("apikey", out var apiKey))
        {
            throw new UnauthorizedAccessException("The API Key is missing");
        }
        if (apiKey != expectedApiKey)
        {
            throw new UnauthorizedAccessException("Invalid API Key for this tenant");
        }
        await next(context);
    }
}