using Microsoft.Extensions.Options;

namespace CoreDriven.Data.Configuration;
public interface IConnectionResolver
{
    string GetConnectionString(string tenant);
    string GetApiKey(string tenant);
}

public class ConnectionResolver(IOptions<TenantsConfiguration> tenantsConfig)
    : IConnectionResolver
{
    public string GetConnectionString(string tenant)
    {
        return tenantsConfig.Value.TryGetValue(tenant, out var tenantConfig)
            ? tenantConfig.ConnectionString
            : throw new KeyNotFoundException($"No connection string found for tenant {tenant}");
    }

    public string GetApiKey(string tenant)
    {
        return tenantsConfig.Value.TryGetValue(tenant, out var tenantConfig)
            ? tenantConfig.ApiKey
            : throw new KeyNotFoundException($"No API key found for tenant {tenant}");
    }
}