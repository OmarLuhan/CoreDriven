using CoreDriven.Data.Repositories;

namespace CoreDriven.Api.Middlewares;

public class TenantMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ITenantProvider tenantProvider)
    {
        // Ejemplo: /{tenant}/api/usuarios
        var tenantId = context.Request.Path.Value?.Split('/')[1]; 

        if (!string.IsNullOrEmpty(tenantId))
        {
            tenantProvider.SetTenant(tenantId);
        }

        await next(context);
    }
}
