using CoreDriven.UseCases.Users.Commands;
using CoreDriven.UseCases.Users.Queries;

namespace CoreDriven.UseCases.Users;

public record UserUseCases(
    GetUsers GetUsers,
    AddUser AddUser);