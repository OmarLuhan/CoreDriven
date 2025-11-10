namespace CoreDriven.Data.Repositories;

public interface ITenantProvider
{
    string? GetTenant();
    void SetTenant(string tenant);
}

public class TenantProvider : ITenantProvider
{
    private string? _tenant;
    public string? GetTenant() => _tenant;
    public void SetTenant(string tenant) => _tenant = tenant;
}
