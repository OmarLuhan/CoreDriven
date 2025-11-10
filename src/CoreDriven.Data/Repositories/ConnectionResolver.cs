using Microsoft.Extensions.Configuration;

namespace CoreDriven.Data.Repositories;
public interface IConnectionResolver
{
    string GetConnectionString(string tenant);
}
public class ConnectionResolver(IConfiguration configuration):IConnectionResolver
{
    public string GetConnectionString(string tenant)
    {
        var conn = configuration.GetConnectionString(tenant);
        return string.IsNullOrEmpty(conn) ? throw new KeyNotFoundException($"No connection string found for tenant {tenant}") : conn;
    }
}