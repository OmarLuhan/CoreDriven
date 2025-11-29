using CoreDriven.Dto.Users;
using Validot;

namespace CoreDriven.UseCases.Validators;

public static class UserValidator
{
    private static readonly IValidator<UserCreateDto> CreateValidator;
    static UserValidator()
    {
        CreateValidator = Validot.Validator.Factory.Create<UserCreateDto>(s => s
            .Member(x => x.Name,x=> x
                .NotEmpty().NotWhiteSpace().WithMessage("Name is required"))
        .Member(x => x.Email, x=> x
                .Email().WithMessage("Email is required")));
    }
    extension(UserCreateDto)
    {
        public static IValidator<UserCreateDto> GetCreateValidator() => CreateValidator;
    }
}