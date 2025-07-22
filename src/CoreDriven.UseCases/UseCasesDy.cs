using CoreDriven.UseCases.Users;
using CoreDriven.UseCases.Users.Commands;
using CoreDriven.UseCases.Users.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDriven.UseCases;

public static class UseCasesDy
{
    public static void AddUseCases(this IServiceCollection service)
    {
        service.AddUserUseCases();
    }
    private static void AddUserUseCases(this IServiceCollection service)
    {
        service.AddScoped<UserUseCases>()
            .AddScoped<GetUsers>()
            .AddScoped<AddUser>();
    }
}