using CoreDriven.Data.Configuration;
using CoreDriven.Data.Providers;
using CoreDriven.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDriven.Data;

public static class DataDi
{
    extension(IServiceCollection service)
    {
        public void AddData()
        {
            service.AddMultiTenant();
            // Register the DbContext with MySQL configuration
            service.AddMysql();
            service.AddDataRepositories();
            service.AddHealthChecksMysql();

        }

        private void AddMultiTenant()
        {
            //services multi-tenant
            service.AddScoped<ITenantProvider, TenantProvider>();
            service.AddSingleton<IConnectionResolver, ConnectionResolver>();
        }

        private void AddDataRepositories()
        {
            service.AddScoped<IUserRepository,UserRepository>();
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        private void AddMysql()
        {
            service.AddDbContext<Entities.AppContext>((sp, options) =>
            {
                var tenantProvider = sp.GetRequiredService<ITenantProvider>();
                var connectionResolver = sp.GetRequiredService<IConnectionResolver>();
                var tenant = tenantProvider.GetTenant();
                if (string.IsNullOrEmpty(tenant))
                    throw new Exception("Tenant not resolved for this request");
                var connectionString = connectionResolver.GetConnectionString(tenant);
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }
        private IServiceCollection AddHealthChecksMysql()
        {
            service.AddHealthChecks()
                .AddMultiTenantMySql(
                    name: "mysql_tenants",
                    tags: ["database", "mysql", "tenants"],
                    timeout: TimeSpan.FromSeconds(10));
            return service;
        }
    }
}