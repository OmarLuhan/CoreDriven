using CoreDriven.Data.Entities;
using CoreDriven.Data.Repositories;
using CoreDriven.Dto.Users;
using CoreDriven.UseCases.Mappers;
using CoreDriven.UseCases.Validator;

namespace CoreDriven.UseCases.Users.Commands;

public class CreateUser(IUserRepository repository)
{
    public async Task<UserDto> Execute(UserCreateDto dto)
    {
        dto.Validate();
        var creation = dto.ToUser();
        creation.Password = "password encrypted"; // Placeholder for password encryption logic
        User user=await repository.AddAsync(creation);
        return user.ToDto();
    }
}