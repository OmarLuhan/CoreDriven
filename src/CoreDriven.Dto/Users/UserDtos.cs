namespace CoreDriven.Dto.Users;

public record UserDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string? ImageUrl { get; init; }
    public string? ImageName { get; init; }
    public int RoleId { get; init; }
    public bool? Active { get; init; }
}