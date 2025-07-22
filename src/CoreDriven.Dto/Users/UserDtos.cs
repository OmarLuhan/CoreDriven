namespace CoreDriven.Dto.Users;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string? ImageUrl,
    string? ImageName,
    int RoleId,
    string RoleName,
    bool? Active);


public record UserCreateDto( 
    string Name,
    string Email,
    int RoleId,
    bool Active = true
    );
