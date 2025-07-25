using CoreDriven.Data.Entities;
using CoreDriven.Data.Repositories;
using CoreDriven.Dto.Users;
using CoreDriven.UseCases.Mappers;

namespace CoreDriven.UseCases.Users.Commands;

public class AddUser(IUserRepository repository)
{
    public async Task<UserDto> Execute(UserCreateDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "User cannot be null.");
        }
        var creation = dto.ToUser();
        creation.Password = "password encrypted"; // Placeholder for password encryption logic
        User user=await repository.AddAsync(creation);
        return user.ToDto();
    }
}