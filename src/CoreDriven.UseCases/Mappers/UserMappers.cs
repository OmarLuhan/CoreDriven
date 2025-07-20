using CoreDriven.Data.Entities;
using CoreDriven.Dto.Users;

namespace CoreDriven.Dto.Mappers;

public static class UserMappers
{
    public static UserDto ToDto(this User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        ImageUrl = user.ImageUrl,
        ImageName = user.ImageName,
        RoleId = user.RoleId,
        Active = user.Active
    };
    public static List<UserDto>ToDto(this IEnumerable<User> users) => 
        users.Select(user => user.ToDto()).ToList();
}