using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace CoreDriven.Data.Configuration;

public static class HealthCheckExtension
{
    public static IHealthChecksBuilder AddMultiTenantMySql(
        this IHealthChecksBuilder builder,
        string name = "mysql_tenants",
        HealthStatus? failureStatus = null,
        IEnumerable<string>? tags = null,
        TimeSpan? timeout = null)
    {
        return builder.Add(new HealthCheckRegistration(
            name,
            sp => new MultiTenantMySqlHealthCheck(
                sp.GetRequiredService<IOptions<TenantsConfiguration>>()),
            failureStatus,
            tags,
            timeout));
    }
}