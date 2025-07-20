using CoreDriven.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDriven.Data;

public static class DataDy
{
    public static void AddData(this IServiceCollection service, IConfiguration configuration)
    {
        // Register the DbContext with MySQL configuration
        service.AddMysql(configuration);
        service.AddDataRepositories();
    }
    private static void AddDataRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository,UserRepository>();
        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
    private static void AddMysql(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<Entities.AppContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.Parse("8.0.42-mysql")));
    }
}