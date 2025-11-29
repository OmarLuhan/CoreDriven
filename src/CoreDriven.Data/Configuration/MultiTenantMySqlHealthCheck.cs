using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace CoreDriven.Data.Configuration;

public class MultiTenantMySqlHealthCheck(IOptions<TenantsConfiguration> tenantsConfig):IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var unhealthyTenants = new List<string>();
        var healthyTenants = new List<string>();
        var data = new Dictionary<string, object>();

        foreach (var (tenantName, config) in tenantsConfig.Value)
        {
            try
            {
                await using var connection = new MySqlConnection(config.ConnectionString);
                await connection.OpenAsync(cancellationToken);
                await using var command = connection.CreateCommand();
                command.CommandText = "SELECT 1";
                await command.ExecuteScalarAsync(cancellationToken);
                
                healthyTenants.Add(tenantName);
                data[$"tenant_{tenantName}"] = "Healthy";
            }
            catch (Exception ex)
            {
                unhealthyTenants.Add(tenantName);
                data[$"tenant_{tenantName}"] = $"Unhealthy: {ex.Message}";
            }
        }

        data["healthy_count"] = healthyTenants.Count;
        data["unhealthy_count"] = unhealthyTenants.Count;
        data["total_tenants"] = tenantsConfig.Value.Count;

        if (unhealthyTenants.Count == 0)
        {
            return HealthCheckResult.Healthy(
                $"All {healthyTenants.Count} tenant databases are healthy",
                data);
        }
        
        if (healthyTenants.Count == 0)
        {
            return HealthCheckResult.Unhealthy(
                "All tenant databases are unhealthy",
                data: data);
        }

        return HealthCheckResult.Degraded(
            $"{unhealthyTenants.Count} of {tenantsConfig.Value.Count} tenant databases are unhealthy: {string.Join(", ", unhealthyTenants)}",
            data: data);
    }
}