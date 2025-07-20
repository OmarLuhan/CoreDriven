using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDriven.Data;

public static class DataDy
{
    public static void AddData(this IServiceCollection services, IConfiguration config)
    {
        // Register the DbContext with MySQL configuration
        services.AddDbContext<Entities.AppContext>(options =>
            options.UseMySql(config.GetConnectionString("DefaultConnection"),
                ServerVersion.Parse("8.0.42-mysql")));
    }
}