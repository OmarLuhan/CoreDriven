namespace CoreDriven.Data.Configuration;

public record TenantConfiguration(string ConnectionString, string ApiKey);

public class TenantsConfiguration : Dictionary<string, TenantConfiguration>;