using System.Linq.Expressions;
using CoreDriven.Data.Entities;
using CoreDriven.Dto.Users;

namespace CoreDriven.UseCases.Mappers;

public static class UserMappers
{
    public static Expression<Func<User, UserDto>> ToDtoExpr => user => new UserDto
    (
        user.Id,
        user.Name,
        user.Email,
        user.ImageUrl,
        user.ImageName,
        user.RoleId,
        user.Role.Value,
        user.Active
    );
    public static UserDto ToDto(this User user) => new
    (
        user.Id,
        user.Name,
        user.Email,
        user.ImageUrl,
        user.ImageName,
        user.RoleId,
        user.Role?.Value ?? "",
        user.Active
    );
    public static User ToUser(this UserCreateDto user) => new()
    {
        Name = user.Name,
        Email = user.Email,
        RoleId = user.RoleId,
        Active = user.Active
    };

}