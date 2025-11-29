using CoreDriven.Data.Providers;

namespace CoreDriven.Api.Middlewares;

public class TenantMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ITenantProvider tenantProvider)
    {
        // Tomar tenant desde header
        var tenantId = context.Request.Headers["tenant"].ToString();
        if (!string.IsNullOrWhiteSpace(tenantId))
        {
            tenantProvider.SetTenant(tenantId);
        }
        if (!string.IsNullOrEmpty(tenantId))
            tenantProvider.SetTenant(tenantId);

        await next(context);
    }
}

