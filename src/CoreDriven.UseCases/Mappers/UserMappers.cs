using System.Linq.Expressions;
using CoreDriven.Data.Entities;
using CoreDriven.Dto.Users;

namespace CoreDriven.UseCases.Mappers;

public static class UserMappers
{
    public static Expression<Func<User, UserDto>> ToDto => user => new UserDto
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        ImageUrl = user.ImageUrl,
        ImageName = user.ImageName,
        RoleId = user.RoleId,
        Active = user.Active
    };

}