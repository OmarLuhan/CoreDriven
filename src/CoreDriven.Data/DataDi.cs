using CoreDriven.Data.Configuration;
using CoreDriven.Data.Providers;
using CoreDriven.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDriven.Data;

public static class DataDi
{
    public static void AddData(this IServiceCollection service)
    {
        service.AddMultiTenant();
        // Register the DbContext with MySQL configuration
        service.AddMysql();
        service.AddDataRepositories();
        
    }
    private static void AddMultiTenant(this IServiceCollection services)
    {
        //services multi-tenant
        services.AddScoped<ITenantProvider, TenantProvider>();
        services.AddSingleton<IConnectionResolver, ConnectionResolver>();
    }
    private static void AddDataRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository,UserRepository>();
        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
    private static void AddMysql(this IServiceCollection service)
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
}