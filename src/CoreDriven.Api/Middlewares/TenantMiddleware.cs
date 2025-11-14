using CoreDriven.Data.Providers;

namespace CoreDriven.Api.Middlewares;

public class TenantMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ITenantProvider tenantProvider)
    {
        // Ejemplo: /{tenant}/api/usuarios
        var segments = context.Request.Path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        
        if (segments is { Length: > 0 })
        {
            var tenantId = segments[0];
            tenantProvider.SetTenant(tenantId);
        }
        await next(context);
    }
}

